using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace TowerDefenseGame.GameEntity
{
    public class TowerC : AbstractTower
    {
        public UnityEvent<Monster> onSlowTarget;

        [Header("Special Ammo")] [SerializeField]
        private float enemySpeedDecrease = 0.35f;

        /// <summary>
        /// Target closest enemy
        /// Decrease enemy speed by 35%
        /// </summary>
        /// <param name="monsters"></param>
        public override void TryAttackTarget(List<Monster> monsters)
        {
            var targetMonster = GetClosestMonsterInRange(monsters);
            if (!targetMonster) return;
            LookAtTarget(targetMonster);
            if (!CanFire()) return;
            SetFireDelay();

            DamageMonster(targetMonster);
            SlowTarget(targetMonster);
            this.Log($"{name} Fire!");
        }

        private void SlowTarget(Monster monster)
        {
            var baseSpeed = monster.GetBaseSpeed();
            var decreaseSpeed = baseSpeed * enemySpeedDecrease;
            var modifiedSpeed = baseSpeed - decreaseSpeed;
            monster.SetSpeed(modifiedSpeed);
            this.Log($"Modified Speed: {modifiedSpeed}/{baseSpeed} (-{enemySpeedDecrease * 100f}%)");
        }
    }
}