using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.GameEntity
{
    public class TowerB : AbstractTower
    {
        [Header("Special Ammo")] [SerializeField]
        private float dealDamageArea = 1f;

        /// <summary>
        /// Target farthest, Most hp enemy
        /// Damage around area 1m
        /// </summary>
        /// <param name="monsters"></param>
        public override void TryAttackTarget(List<Monster> monsters)
        {
            var targetMonster = GetFarthestAndMostHealthMonster(monsters);
            if (!targetMonster) return;
            LookAtTarget(targetMonster);
            if (!InFireRange(targetMonster)) return;
            if (!CanFire()) return;
            SetFireDelay();
            DoAreaDamage(monsters);
            this.Log($"{name} Fire!");
        }

        private void DoAreaDamage(List<Monster> monsters)
        {
            var damagePos = transform.position;
            foreach (var monster in monsters)
            {
                if (!IsInDamageArea(damagePos, dealDamageArea, monster)) continue;
                DamageMonster(monster);
            }
        }

        private bool IsInDamageArea(Vector3 damagePos, float damageArea, Monster target)
        {
            return Vector3.Distance(damagePos, target.transform.position) < damageArea;
        }
    }
}