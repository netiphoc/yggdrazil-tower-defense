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
        [Header("Wave")] [SerializeField] private float waveUpdateInterval;

        public MapManager MapManager { get; private set; }
        public WaypointManager WaypointManager { get; private set; }
        public MonsterSpawner MonsterSpawner { get; private set; }
        public TowerSpawner TowerSpawner { get; private set; }
        public WaveManager WaveManager { get; private set; }

        private void Awake()
        {
            mapSo.DebugAssert();
            monsterPrefabSo.DebugAssert();
            towerPrefabSo.DebugAssert();
        }

        public void InitializeGame()
        {
            MonsterSpawner?.Dispose();
            TowerSpawner?.Dispose();

            MapManager = new MapManager(mapSo);
            WaypointManager = new WaypointManager(mapSo);
            MonsterSpawner = new MonsterSpawner(monsterPrefabSo);
            TowerSpawner = new TowerSpawner(towerPrefabSo);
            WaveManager = new WaveManager(waveUpdateInterval > 0f ? waveUpdateInterval : 1f);
        }
    }
}