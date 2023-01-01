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

        private int _monsterToSpawn;
        private float _currentSpawnDelay;
        private float _difficultyToAdd;

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
            GameController.ResetDifficulty();
            NextWave();
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
            var randomPathIndex = Random.Range(0, GameManager.WaypointManager.WaypointPaths.Length);
            var waypointPath = GameManager.WaypointManager.WaypointPaths[randomPathIndex];
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
            var increaseSpeed = monster.GetSpeed() * GameController.DifficultPercent;
            var newMaxSpeed = monster.GetSpeed() + increaseSpeed;
            this.Log(
                $"{monster.GetEntityType()} ModifyDifficultySpeed: {monster.GetMaxSpeed()} -> {newMaxSpeed} +{increaseSpeed}");
            monster.SetMaxSpeed(newMaxSpeed);
            monster.SetSpeed(monster.GetMaxSpeed());
        }

        /// <summary>
        /// Increase monster health by x percent every wave
        /// </summary>
        /// <param name="monster"></param>
        private void ModifyDifficultyHealth(Monster monster)
        {
            var increaseHealth = monster.GetHealth() * GameController.DifficultPercent;
            var newMaxHealth = monster.GetHealth() + increaseHealth;
            this.Log(
                $"{monster.GetEntityType()} ModifyDifficultyHealth: {monster.GetMaxHealth()}->{newMaxHealth} +{increaseHealth}");
            monster.SetMaxHealth(newMaxHealth);
            monster.SetHealth(monster.GetMaxHealth());
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
            IncreaseDifficultyPerWave();
        }

        private void IncreaseDifficultyPerWave()
        {
            if (_difficultyToAdd > 0f)
            {
                GameController.SetDifficulty(GameController.DifficultPercent + _difficultyToAdd);
                _difficultyToAdd = 0f;
            }

            _difficultyToAdd = _difficultyIncreasePerWave;
        }

        #endregion
    }
}