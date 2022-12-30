using UnityEngine;
using Utilities;

namespace TowerDefenseGame.GameState
{
    public class InGameState : State
    {
        private readonly int _spawnCount;
        private readonly float _waveDuration;
        private readonly float _difficultyIncreasePerWave;

        public InGameState(GameController gameController, int spawnCount, float waveDuration,
            float difficultyIncreasePerWave) : base(gameController)
        {
            _spawnCount = spawnCount;
            _waveDuration = waveDuration;
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

        private void SpawnWaveMonster()
        {
            var waypointPath = GameManager.WaypointManager.WaypointPaths[0];
            for (var i = 0; i < _spawnCount; i++)
            {
                var monster = GameManager.MonsterSpawner.SpawnRandomMonster();
                monster.SetPath(waypointPath.path);

                // Increase monster speed by x percent every wave
                var increaseSpeed = monster.GetSpeed() * GameController.DifficultPercent;
                var modifiedSpeed = monster.GetSpeed() + increaseSpeed;
                monster.SetSpeed(modifiedSpeed);

                // Increase monster health by x percent every wave
                var increaseHealth = monster.GetHealth() * GameController.DifficultPercent;
                var modifiedHealth = monster.GetHealth() + increaseHealth;
                monster.SetHealth(modifiedHealth);
            }

            GameController.InCreaseDifficulty(_difficultyIncreasePerWave);
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
            SpawnWaveMonster();
        }

        #endregion
    }
}