using TowerDefenseGame.Map.ScriptableObjects;


namespace TowerDefenseGame.Waypoint
{
    public class WaypointManager
    {
        public WaypointPath[] WaypointPaths { get; }

        public WaypointManager(MapSo mapSo)
        {
            WaypointPaths = mapSo.GetMapConfigSo().GetPaths();
        }
    }
}