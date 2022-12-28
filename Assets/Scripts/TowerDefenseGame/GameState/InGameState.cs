using UnityEngine;

namespace TowerDefenseGame.GameState
{
    public class InGameState : State
    {
        private readonly int _spawnCount;
        private readonly float _spawnDelay;
        private readonly float _difficultyIncreasePerWave;

        private float _currentDelay;

        public InGameState(GameController gameController, int spawnCount, float spawnDelay,
            float difficultyIncreasePerWave) : base(gameController)
        {
            _spawnCount = spawnCount;
            _spawnDelay = spawnDelay;
            _difficultyIncreasePerWave = difficultyIncreasePerWave;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            UpdateMonsterSpawner();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            UpdateMonster();
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

        private void UpdateMonsterSpawner()
        {
            if (_currentDelay > 0f)
            {
                _currentDelay -= Time.deltaTime;
                return;
            }

            _currentDelay = _spawnDelay;
            SpawnWaveMonster();
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
    }
}