﻿using UnityEngine;

namespace TowerDefenseGame.GameEntity
{
    public interface ILivingEntity : IDamageAble
    {
        float GetSpeed();
        float GetBaseSpeed();
        void SetSpeed(float value);
        void ResetSpeed();
        void SetPath(Vector3[] path);
        void UpdateMoveToPath();
        bool IsDestinationReached();
    }

    public class LivingEntity : DamageAble, ILivingEntity
    {
        private const float ReachedDist = 0.05f;

        [SerializeField] private float initSpeed;
        private float _speed;

        private Vector3[] _path;
        private int _pathIndex;
        private Vector3 _offset;

        protected override void Awake()
        {
            base.Awake();
            _speed = initSpeed;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public float GetBaseSpeed()
        {
            return initSpeed;
        }

        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public void ResetSpeed()
        {
            _speed = initSpeed;
        }

        public void SetPath(Vector3[] path)
        {
            if (path.Length == 0) return;
            _offset = Random.insideUnitSphere * 0.5f;
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