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
        [Header("Enemy")] [SerializeField] private MonsterPrefabSo monsterPrefabSo;
        [Header("Tower")] [SerializeField] private TowerPrefabSo towerPrefabSo;

        public MapManager MapManager { get; private set; }
        public WaypointManager WaypointManager { get; private set; }
        public MonsterSpawner MonsterSpawner { get; private set; }
        public TowerSpawner TowerSpawner { get; private set; }

        private void Awake()
        {
            mapSo.DebugAssert();
            monsterPrefabSo.DebugAssert();
            towerPrefabSo.DebugAssert();

            MapManager = new MapManager(mapSo);
            WaypointManager = new WaypointManager(mapSo);
            MonsterSpawner = new MonsterSpawner(monsterPrefabSo);
            TowerSpawner = new TowerSpawner(towerPrefabSo);
        }
    }
}