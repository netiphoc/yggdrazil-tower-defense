using UnityEngine;

namespace Utilities
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _camTransform;

        private void Awake()
        {
            _camTransform = Camera.main.transform;
        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.LookRotation(_camTransform.forward);
        }
    }
}