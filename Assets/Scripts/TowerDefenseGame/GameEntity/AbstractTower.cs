using System;
using System.Collections.Generic;
using TowerDefenseGame.GameEntity.ScriptableObjects;
using TowerDefenseGame.Map;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Random = UnityEngine.Random;

namespace TowerDefenseGame.GameEntity
{
    public abstract class AbstractTower : DamageAble, ITower
    {
        public UnityEvent<Monster> onAttackEnemy;

        [Header("Tower Config")] [SerializeField]
        private EntityTypeSo targetEntityType;

        [SerializeField, TextArea] private string towerInfo;
        [Space] [SerializeField] private float fireRate;
        [SerializeField] private float fireRange;

        [Space] [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [Space] [SerializeField] private float lookTargetSpeed;
        [SerializeField] private float damageMultiplier = 1.5f;

        protected Grid<Block> Grid;
        private float _currentFireDelay;

        private void Start()
        {
            Grid = FindObjectOfType<GameManager>().MapManager.Grid;
        }

        public string GetInfo()
        {
            return towerInfo;
        }

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
            return fireRange + 0.5f;
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
            return GetDistance(monster) <= GetFireRange();
        }

        protected Monster GetClosestMonsterInRange(List<Monster> monsters, bool closest = true)
        {
            if (monsters.Count == 0) return null;
            var closestMonster = monsters[0];
            var closestDistSq = closest ? float.MaxValue : float.MinValue;
            foreach (var monster in monsters)
            {
                if (!InFireRange(monster)) continue;
                var distSq = GetDistanceSq(monster);
                var closet = closest ? distSq < closestDistSq : distSq > closestDistSq;
                if (!closet) continue;
                closestMonster = monster;
                closestDistSq = distSq;
            }

            return closestMonster;
        }

        protected Monster GetFarthestMonsterInRange(List<Monster> monsters)
        {
            return GetClosestMonsterInRange(monsters, false);
        }

        protected Monster GetClosestMostHpMonsterInRange(List<Monster> monsters, bool closest = true)
        {
            if (monsters.Count == 0) return null;
            var closestMonster = monsters[0];
            var closestDistSq = closest ? float.MaxValue : float.MinValue;
            var mostHp = float.MinValue;
            foreach (var monster in monsters)
            {
                if (!InFireRange(monster)) continue;
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

        protected Monster GetFarthestAndMostHealthMonsterInRange(List<Monster> monsters)
        {
            return GetClosestMostHpMonsterInRange(monsters, false);
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
            onAttackEnemy?.Invoke(monster);
        }

        #endregion

        #region Grid

        protected Block[] FindBlockAroundPosition(Vector3 position, float range, bool ignoreTowerBlock = true)
        {
            var inRangeBlocks = new List<Block>();
            var targetBlock = GetBlock(position, false);
            if (!targetBlock)
            {
                this.Log($"No target block at {position}");
                return inRangeBlocks.ToArray();
            }

            for (var x = 0; x < Grid.Width; x++)
            {
                for (var y = 0; y < Grid.Height; y++)
                {
                    var block = Grid.GetElementAt(x, y);
                    if (ignoreTowerBlock && block is TowerBlock) continue;
                    if (Vector3.Distance(block.Position, targetBlock.Position) > range) continue;
                    inRangeBlocks.Add(block);
                }
            }

            return inRangeBlocks.ToArray();
        }

        protected Block GetBlock(Vector3 position, bool ignoreTowerBlock = true)
        {
            var block = Grid.GetElementAt(position);
            if (ignoreTowerBlock && block && block is TowerBlock)
            {
                return null;
            }

            return block;
        }

        #endregion
    }
}