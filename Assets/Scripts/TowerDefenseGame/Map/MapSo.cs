using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenseGame.Map
{
    [CreateAssetMenu(fileName = "New " + nameof(MapSo), menuName = "Tower Defense/Map/" + nameof(MapSo), order = 0)]
    public class MapSo : ScriptableObject
    {
        [SerializeField] private RawImage mapImage;
        [SerializeField] private int mapWidth;
        [SerializeField] private int mapHeight;
        public RawImage MapRawImage => mapImage;
        public int Width => mapWidth;
        public int Height => mapHeight;
    }
}