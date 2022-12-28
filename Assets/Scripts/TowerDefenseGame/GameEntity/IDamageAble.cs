using UnityEngine;
using UnityEngine.Events;

namespace TowerDefenseGame.GameEntity
{
    public interface IDamageAble : IEntity
    {
        void Damage(float amount);
        float GetHealth();
        void SetHealth(float health);
    }

    public class DamageAble : Entity, IDamageAble
    {
        public UnityEvent<Entity> onEntityDead;
        [SerializeField] private float health;

        public void Damage(float amount)
        {
            if (amount < 0f) return;
            health -= amount;
            if (health > 0f) return;
            onEntityDead?.Invoke(this);
        }

        public float GetHealth()
        {
            return health;
        }

        public void SetHealth(float hp)
        {
            health = hp;
        }
    }
}