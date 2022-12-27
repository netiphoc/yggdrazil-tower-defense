using TowerDefenseGame.Map;
using TowerDefenseGame.Map.ScriptableObjects;
using TowerDefenseGame.Waypoint;
using UnityEngine;

namespace TowerDefenseGame
{
    public class GameManager : MonoBehaviour
    {
        [Header("Map")] [SerializeField] private MapSo mapSo;
        public MapManager MapManager { get; private set; }
        public WaypointManager WaypointManager { get; private set; }

        private void Awake()
        {
            MapManager = new MapManager(mapSo);
            WaypointManager = new WaypointManager(mapSo);
        }
    }
}