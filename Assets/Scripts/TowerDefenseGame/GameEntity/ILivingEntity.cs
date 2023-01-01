using UnityEngine;
using UnityEngine.Events;

namespace TowerDefenseGame.GameEntity
{
    public interface ILivingEntity : IDamageAble
    {
        float GetSpeed();
        float GetMaxSpeed();
        void SetMaxSpeed(float value);
        void SetSpeed(float value);
        void ResetSpeed();
        void SetPath(Vector3[] path);
        void UpdateMoveToPath();
        bool IsDestinationReached();
    }

    public class LivingEntity : DamageAble, ILivingEntity
    {
        public UnityEvent<float> onSpeedChanged;

        private const float ReachedDist = 0.05f;
        private const float RandomSpawnRange = 0.45f;

        [SerializeField] private float initSpeed;

        private float _speed;
        private float _maxSpeed;

        private Vector3[] _path;
        private int _pathIndex;
        private Vector3 _offset;

        protected override void Awake()
        {
            base.Awake();
            ResetSpeed();
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public float GetMaxSpeed()
        {
            return _maxSpeed;
        }

        public void SetMaxSpeed(float speed)
        {
            _maxSpeed = speed;
        }

        public void SetSpeed(float value)
        {
            _speed = value;
            onSpeedChanged?.Invoke(value);
        }

        public void ResetSpeed()
        {
            _maxSpeed = initSpeed;
            _speed = initSpeed;
        }

        public void SetPath(Vector3[] path)
        {
            if (path.Length == 0) return;
            _offset = Random.insideUnitSphere * RandomSpawnRange;
            _offset.y = 0f;
            _pathIndex = 0;
            _path = path;
            transform.position = _path[0];
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
            transform.position = Vector3.MoveTowards(transform.position, targetPos, GetSpeed() * Time.deltaTime);
        }

        private bool IsPositionReached(Vector3 pos)
        {
            return Vector3.Distance(transform.position, pos) < ReachedDist;
        }

        public bool IsDestinationReached()
        {
            return _pathIndex >= _path.Length;
        }

        public override void ResetEntity()
        {
            base.ResetEntity();
            ResetSpeed();
        }
    }
}