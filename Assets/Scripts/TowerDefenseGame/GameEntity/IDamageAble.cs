using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace TowerDefenseGame.GameEntity
{
    public interface IDamageAble : IEntity
    {
        void Damage(float amount);
        float GetHealth();
        float GetBaseHealth();
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
            SetHealth(GetHealth() - amount);
            this.Log($"Damage {name}: {amount} | {GetHealth():F0}/{GetBaseHealth()}");
            if (GetHealth() > 0f) return;
            Dead = true;
            onEntityDead?.Invoke(this);
            this.Log($"{name} is dead!");
        }

        public float GetHealth()
        {
            return _health;
        }

        public float GetBaseHealth()
        {
            return initHealth;
        }

        public void SetHealth(float hp)
        {
            _health = Mathf.Clamp(hp, 0f, GetBaseHealth());
        }

        public override void ResetEntity()
        {
            base.ResetEntity();

            Dead = false;
            _health = initHealth;
        }
    }
}