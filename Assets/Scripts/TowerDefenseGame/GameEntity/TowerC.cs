using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.GameEntity
{
    public class TowerC : AbstractTower
    {
        [Header("Special Ammo")] [SerializeField]
        private float enemySpeedDecrease = 0.35f;

        /// <summary>
        /// Target closest enemy
        /// Decrease enemy speed by 35%
        /// </summary>
        /// <param name="monsters"></param>
        public override void TryAttackTarget(List<Monster> monsters)
        {
            var targetMonster = GetClosestMonster(monsters);
            if (!targetMonster) return;
            LookAtTarget(targetMonster);
            if (!InFireRange(targetMonster)) return;
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