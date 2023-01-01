using TowerDefenseGame.GameEntity;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.GameState
{
    public class InGameState : State
    {
        private readonly int _spawnCount;
        private readonly float _waveDuration;
        private readonly float _difficultyIncreasePerWave;
        private readonly float _spawnDelay;
        private float _monsterToSpawn;

        private float _currentSpawnDelay;
        private float _currentDifficulty;

        public InGameState(GameController gameController, int spawnCount, float waveDuration, float spawnDelay,
            float difficultyIncreasePerWave) : base(gameController)
        {
            _spawnCount = spawnCount;
            _waveDuration = waveDuration;
            _spawnDelay = spawnDelay;
            _difficultyIncreasePerWave = difficultyIncreasePerWave;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            NextWave();
            GameController.ResetDifficulty();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            UpdateWave();
            UpdateMonster();
            CheckSpawnWaveMonster();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            UpdateTower();
        }

        #region Monster

        private void UpdateMonster()
        {
            var spawnedMonster = GameManager.MonsterSpawner.GetSpawnedMonster();
            for (var i = 0; i < spawnedMonster.Count; i++)
            {
                var monster = spawnedMonster[i];

                if (monster.Dead ||
                    monster.IsDestinationReached())
                {
                    GameManager.MonsterSpawner.DeSpawn(monster);
                    continue;
                }

                monster.UpdateMoveToPath();
            }
        }

        private void AddSpawnRandomMonster(int amount)
        {
            _monsterToSpawn += amount;
        }

        private void CheckSpawnWaveMonster()
        {
            if (_monsterToSpawn == 0) return;
            if (_currentSpawnDelay > 0f)
            {
                _currentSpawnDelay -= Time.deltaTime;
                return;
            }

            _currentSpawnDelay = _spawnDelay;
            _monsterToSpawn--;

            SpawnRandomMonster();
        }

        private void SpawnRandomMonster()
        {
            var waypointPath = GameManager.WaypointManager.WaypointPaths[0];
            var monster = GameManager.MonsterSpawner.SpawnRandomMonster();
            monster.SetPath(waypointPath.path);
            ModifyDifficultySpeed(monster);
            ModifyDifficultyHealth(monster);
        }

        /// <summary>
        ///  Increase monster speed by x percent every wave
        /// </summary>
        /// <param name="monster"></param>
        private void ModifyDifficultySpeed(Monster monster)
        {
            monster.SetMaxSpeed(monster.GetSpeed());
            var increaseSpeed = monster.GetSpeed() * _currentDifficulty;
            var modifiedSpeed = monster.GetSpeed() + increaseSpeed;
            monster.SetSpeed(modifiedSpeed);
            this.Log("ModifyDifficultySpeed");
        }

        /// <summary>
        /// Increase monster health by x percent every wave
        /// </summary>
        /// <param name="monster"></param>
        private void ModifyDifficultyHealth(Monster monster)
        {
            monster.SetMaxHealth(monster.GetHealth());
            var increaseHealth = monster.GetHealth() * _currentDifficulty;
            var modifiedHealth = monster.GetHealth() + increaseHealth;
            monster.SetHealth(modifiedHealth);
            this.Log("ModifyDifficultyHealth");
        }

        #endregion

        #region Tower

        private void UpdateTower()
        {
            foreach (var tower in GameManager.TowerSpawner.GetSpawnedTowers())
            {
                tower.TryAttackTarget(GameManager.MonsterSpawner.GetSpawnedMonster());
            }
        }

        #endregion

        #region Wave

        private void UpdateWave()
        {
            var wave = GameManager.WaveManager;
            wave.UpdateWave();

            if (!wave.WaveTimer.IsCountdownOver) return;
            NextWave();
        }


        private void NextWave()
        {
            var wave = GameManager.WaveManager;
            wave.StartWave(_waveDuration);
            wave.Wave++;
            AddSpawnRandomMonster(_spawnCount);
            _currentDifficulty = GameController.DifficultPercent;
            GameController.InCreaseDifficulty(_difficultyIncreasePerWave);
        }

        #endregion
    }
}