using UnityEngine;

namespace System
{
    public interface IPool<T> where T : MonoBehaviour
    {
        T Request();
        void Return(T t);
    }
}