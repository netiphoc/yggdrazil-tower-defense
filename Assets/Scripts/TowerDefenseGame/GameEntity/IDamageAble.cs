using UnityEngine;
using UnityEngine.Events;
using Utilities;

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
        [SerializeField] private float initHealth;
        private float _health;
        public bool Dead { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _health = initHealth;
        }

        public void Damage(float amount)
        {
            if (amount < 0f) return;
            _health -= amount;
            this.Log($"Damage {name}: {amount} | {_health}/{initHealth}");
            if (_health > 0f) return;
            Dead = true;
            onEntityDead?.Invoke(this);
            this.Log($"{name} is dead!");
        }

        public float GetHealth()
        {
            return _health;
        }

        public void SetHealth(float hp)
        {
            _health = hp;
        }

        public override void ResetEntity()
        {
            base.ResetEntity();

            Dead = false;
            _health = initHealth;
        }
    }
}