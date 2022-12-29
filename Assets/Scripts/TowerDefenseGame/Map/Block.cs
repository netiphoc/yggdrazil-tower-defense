using UnityEngine;

namespace TowerDefenseGame.Map
{
    public class Block : MonoBehaviour
    {
        public GameObject PlacedObject { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector3 Position { get; private set; }

        public void SetBlock(int x, int y)
        {
            X = x;
            Y = y;
            Position = new Vector3(X, 0, Y);
        }
    }
}