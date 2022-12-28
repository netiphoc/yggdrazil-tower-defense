using UnityEngine;

namespace TowerDefenseGame.GameEntity
{
    public interface ILivingEntity : IDamageAble
    {
        float GetSpeed();
        void SetSpeed(float value);
        void SetPath(Vector3[] path);
        void UpdateMoveToPath();
        bool IsDestinationReached();
    }

    public class LivingEntity : DamageAble, ILivingEntity
    {
        private const float ReachedDist = 0.05f;

        [SerializeField] private float speed;

        private Vector3[] _path;
        private int _pathIndex;
        private Vector3 _offset;

        public float GetSpeed()
        {
            return speed;
        }

        public void SetSpeed(float value)
        {
            speed = value;
        }

        public void SetPath(Vector3[] path)
        {
            _offset = Random.insideUnitSphere * 0.5f;
            _offset.y = 0f;
            _pathIndex = 0;
            _path = path;
        }

        public void UpdateMoveToPath()
        {
            if (IsDestinationReached()) return;

            var currentPos = _path[_pathIndex] + _offset;
            if (IsPositionReached(currentPos))
            {
                _pathIndex++;
                return;
            }

            MoveTo(currentPos);
        }

        private void MoveTo(Vector3 targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        private bool IsPositionReached(Vector3 pos)
        {
            return Vector3.Distance(transform.position, pos) < ReachedDist;
        }

        public bool IsDestinationReached()
        {
            return _pathIndex >= _path.Length;
        }
    }
}