using TowerDefenseGame.Waypoint;
using UnityEngine;

namespace TowerDefenseGame.Map.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New " + nameof(MapConfigSo), menuName = "Tower Defense/" + nameof(MapConfigSo),
        order = 0)]
    public class MapConfigSo : ScriptableObject
    {
        [SerializeField, Tooltip(
             "[CONFIG]" +
             "\nFile Format: PSD,PNG,JPG" +
             "\nNon-Power of 2: None" +
             "\nRead/Write: Checked" +
             "\nWrap Mode: Clamp" +
             "\nFilter Mode: Point (no filter)" +
             "\nResize Algorithm: Bilinear")]
        private Texture2D mapTexture2D;

        public Texture2D GetMapTexture2D()
        {
            return mapTexture2D;
        }

        [SerializeField] private WaypointPath[] paths;

        public WaypointPath[] GetPaths()
        {
            return paths;
        }
    }
}