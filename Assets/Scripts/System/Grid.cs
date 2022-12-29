using UnityEngine;
using Utilities;

namespace System
{
    public class Grid<TElement> where TElement : MonoBehaviour
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }

        private TElement[,] _elements;

        public Grid(int width, int height, float cellSize)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;

            _elements = new TElement[width, height];
        }

        public TElement GetElementAt(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                this.Log($"No {x} | {y}");
                return null;
            }

            return _elements[x, y];
        }

        public TElement GetElementAt(float x, float y)
        {
            return GetElementAt(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
        }

        public TElement GetElementAt(Vector3 position)
        {
            return GetElementAt(Mathf.Abs(position.x), Mathf.Abs(position.z));
        }

        public void SetElementAt(int x, int y, TElement element)
        {
            _elements[x, y] = element;
        }
    }
}