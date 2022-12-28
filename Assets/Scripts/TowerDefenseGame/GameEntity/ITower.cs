using TowerDefenseGame.GameEntity.ScriptableObjects;
using UnityEngine;

namespace TowerDefenseGame.GameEntity
{
    public interface ITower
    {
        float GetFireRate();
        void SetFireRate(float fireRate);
        float GetDamage();
        float GetMinDamage();
        void SetMinDamage(float minDamage);
        float GetMaxDamage();
        void SetMaxDamage(float maxDamage);
        float GetFireRange();
        void SetFireRange(float fireRange);

        EntityTypeSo GetTargetEntityType();
    }

    public abstract class AbstractTower : DamageAble, ITower
    {
        [Header("Tower Config")] [SerializeField]
        private EntityTypeSo targetEntityType;

        [SerializeField] private float fireRate;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [SerializeField] private float fireRange;

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
            return fireRange;
        }

        public void SetFireRange(float towerFireRange)
        {
            fireRange = towerFireRange;
        }

        public EntityTypeSo GetTargetEntityType()
        {
            return targetEntityType;
        }
    }
}