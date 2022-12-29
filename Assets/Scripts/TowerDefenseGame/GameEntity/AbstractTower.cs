using System.Collections.Generic;
using TowerDefenseGame.GameEntity.ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Random = UnityEngine.Random;

namespace TowerDefenseGame.GameEntity
{
    public abstract class AbstractTower : DamageAble, ITower
    {
        public UnityEvent<Monster> onAttackMonster;

        [Header("Tower Config")] [SerializeField]
        private EntityTypeSo targetEntityType;

        [Space] [SerializeField] private float fireRate;
        [SerializeField] private float fireRange;

        [Space] [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [Space] [SerializeField] private float lookTargetSpeed;
        [SerializeField] private float damageMultiplier = 1.5f;

        private float _currentFireDelay;

        public float GetFireRate()
        {
            return fireRate;
        }

        public void SetFireRate(float towerFireRate)
        {
            fireRate = towerFireRate;
        }

        public float GetDamage()
        {
            return Random.Range(minDamage, maxDamage);
        }

        public float GetMinDamage()
        {
            return minDamage;
        }

        public void SetMinDamage(float minTowerDamage)
        {
            minDamage = minTowerDamage;
        }

        public float GetMaxDamage()
        {
            return maxDamage;
        }

        public void SetMaxDamage(float maxTowerDamage)
        {
            maxDamage = maxTowerDamage;
        }

        public float GetFireRange()
        {
            return fireRange * rangeMultiplier;
        }

        public void SetFireRange(float towerFireRange)
        {
            fireRange = towerFireRange;
        }

        public EntityTypeSo GetTargetEntityType()
        {
            return targetEntityType;
        }

        public virtual void TryAttackTarget(List<Monster> monsters)
        {
        }

        #region Fucntion

        protected float GetDistanceSq(Monster monster)
        {
            return math.distancesq(transform.position, monster.transform.position);
        }

        protected float GetDistance(Monster monster)
        {
            return Vector3.Distance(transform.position, monster.transform.position);
        }

        protected bool InFireRange(Monster monster)
        {
            return GetDistance(monster) < GetFireRange();
        }

        protected Monster GetClosestMonster(List<Monster> monsters, bool closest = true)
        {
            if (monsters.Count == 0) return null;
            var closestMonster = monsters[0];
            var closestDistSq = GetDistanceSq(monsters[0]);
            foreach (var monster in monsters)
            {
                var distSq = GetDistanceSq(monster);
                var closet = closest ? distSq < closestDistSq : distSq > closestDistSq;
                if (!closet) continue;
                closestMonster = monster;
                closestDistSq = distSq;
            }

            return closestMonster;
        }

        protected Monster GetFarthestMonster(List<Monster> monsters)
        {
            return GetClosestMonster(monsters, false);
        }

        protected Monster GetClosestMostHpMonster(List<Monster> monsters, bool closest = true)
        {
            if (monsters.Count == 0) return null;
            var closestMonster = monsters[0];
            var closestDistSq = GetDistanceSq(monsters[0]);
            var mostHp = monsters[0].GetHealth();
            foreach (var monster in monsters)
            {
                var distSq = GetDistanceSq(monster);
                var closet = closest ? distSq < closestDistSq : distSq > closestDistSq;
                var mostHealth = monster.GetHealth() > mostHp;
                if (!mostHealth) continue;
                if (!closet) continue;
                closestMonster = monster;
                closestDistSq = distSq;
                mostHp = monster.GetHealth();
            }

            return closestMonster;
        }

        protected Monster GetFarthestAndMostHealthMonster(List<Monster> monsters)
        {
            return GetClosestMostHpMonster(monsters, false);
        }

        protected bool CanFire()
        {
            return Time.time > _currentFireDelay;
        }

        protected void SetFireDelay()
        {
            _currentFireDelay = Time.time + GetFireRate();
        }

        protected void LookAtTarget(Monster monster)
        {
            var lookDir = (monster.transform.position - transform.position).normalized;
            lookDir.y = 0f;
            var lookRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, lookTargetSpeed * Time.deltaTime);
        }

        protected void DamageMonster(Monster monster)
        {
            var modifiedDamage = GetDamage();
            var isTargetType = GetTargetEntityType() == monster.GetEntityType();
            if (isTargetType)
            {
                modifiedDamage *= damageMultiplier;
                this.Log($"+{(damageMultiplier - 1f) * 100f}% | {modifiedDamage:F0} damage");
            }

            monster.Damage(modifiedDamage);
            onAttackMonster?.Invoke(monster);
        }

        #endregion
    }
}