using UnityEngine;

namespace TowerDefenseGame.GameState
{
    public class InGameState : State
    {
        public int SpawnCount { get; }
        public float SpawnDelay { get; }

        private float _currentDelay;

        public InGameState(GameController gameController, int spawnCount, float spawnDelay) : base(gameController)
        {
            SpawnCount = spawnCount;
            SpawnDelay = spawnDelay;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            UpdateSpawner();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            UpdateMonster();
        }

        private void UpdateMonster()
        {
            var spawnedMonster = GameManager.MonsterSpawner.GetSpawnedMonster();
            for (var i = 0; i < spawnedMonster.Count; i++)
            {
                var monster = spawnedMonster[i];
                if (monster.IsDestinationReached())
                {
                    GameManager.MonsterSpawner.DeSpawn(monster);
                    continue;
                }

                monster.UpdateMoveToPath();
            }
        }

        private void UpdateSpawner()
        {
            if (_currentDelay > 0f)
            {
                _currentDelay -= Time.deltaTime;
                return;
            }

            _currentDelay = SpawnDelay;
            SpawnWaveMonster();
        }

        private void SpawnWaveMonster()
        {
            var waypointPath = GameManager.WaypointManager.WaypointPaths[0];
            for (var i = 0; i < SpawnCount; i++)
            {
                var monster = GameManager.MonsterSpawner.SpawnRandomMonster();
                monster.SetPath(waypointPath.path);
            }
        }
    }
}