using TowerDefenseGame.Map;
using TowerDefenseGame.Map.ScriptableObjects;
using TowerDefenseGame.Spawner;
using TowerDefenseGame.Waypoint;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame
{
    public class GameManager : MonoBehaviour
    {
        [Header("Map")] [SerializeField] private MapSo mapSo;
        [Header("Enemy")] [SerializeField] private EnemyPrefabSo enemyPrefabSo;
        public MapManager MapManager { get; private set; }
        public WaypointManager WaypointManager { get; private set; }
        public EnemySpawner EnemySpawner { get; private set; }

        private void Awake()
        {
            mapSo.DebugAssert();
            enemyPrefabSo.DebugAssert();

            MapManager = new MapManager(mapSo);
            WaypointManager = new WaypointManager(mapSo);
            EnemySpawner = new EnemySpawner(enemyPrefabSo);
        }
    }
}