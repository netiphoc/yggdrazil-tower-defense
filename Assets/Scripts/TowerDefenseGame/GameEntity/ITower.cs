using System.Collections.Generic;
using TowerDefenseGame.GameEntity.ScriptableObjects;

namespace TowerDefenseGame.GameEntity
{
    public interface ITower
    {
        string GetInfo();
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
        public void TryAttackTarget(List<Monster> monsters);
    }
}