using UnityEngine;

namespace TowerDefenseGame.Map
{
    public class Block : MonoBehaviour
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public void SetBlock(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}