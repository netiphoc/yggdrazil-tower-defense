using UnityEngine;

namespace TowerDefenseGame.Map
{
    public class Block : MonoBehaviour
    {
        public GameObject PlacedObject { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public void SetBlock(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector3 GetWorldPosition()
        {
            return new Vector3(X, 0, Y);
        }
    }
}