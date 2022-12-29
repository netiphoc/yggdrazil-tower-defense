using System;
using System.Collections.Generic;
using TowerDefenseGame.Map;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace TowerDefenseGame.GameEntity
{
    public class TowerB : AbstractTower
    {
        public UnityEvent<Block[]> onExplodeArea;

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

            DoAreaDamage(monsters, targetMonster.transform.position);
            this.Log($"{name} Fire!");
        }

        private void DoAreaDamage(List<Monster> monsters, Vector3 targetPosition)
        {
            var explodeBlocks = FindBlockAroundPosition(targetPosition, dealDamageArea);
            onExplodeArea?.Invoke(explodeBlocks);

            // Do area explode damage
            foreach (var monster in monsters)
            {
                foreach (var block in explodeBlocks)
                {
                    if (!block) continue;
                    var distToTower = Vector3.Distance(block.Position, monster.transform.position);
                    var isInBlock = distToTower <= Grid.CellSize * 0.5f;
                    if (!isInBlock) continue;
                    DamageMonster(monster);
                }
            }
        }
    }
}