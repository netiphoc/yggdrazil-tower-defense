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

            DoAreaDamage(monsters);
            this.Log($"{name} Fire!");
        }

        private void DoAreaDamage(List<Monster> monsters)
        {
            var explodeBlocks = FindExplodeBlock();
            onExplodeArea?.Invoke(explodeBlocks);

            // Do area explode damage
            foreach (var monster in monsters)
            {
                foreach (var block in explodeBlocks)
                {
                    if (!block) continue;
                    var distToTower = Vector3.Distance(new Vector3(block.X, 0, block.Y), monster.transform.position);
                    if (distToTower > 0.5f) continue;
                    DamageMonster(monster);
                }
            }
        }

        private Block[] FindExplodeBlock()
        {
            var gameManager = FindObjectOfType<GameManager>();
            var grid = gameManager.MapManager.Grid;
            var position = transform.position;

            return new[]
            {
                GetBlock(grid, new Vector3(0, 0, dealDamageArea) + position),
                GetBlock(grid, new Vector3(dealDamageArea, 0, 0) + position),
                GetBlock(grid, new Vector3(0, 0, -dealDamageArea) + position),
                GetBlock(grid, new Vector3(-dealDamageArea, 0, 0) + position),
            };
        }

        private Block GetBlock(Grid<Block> grid, Vector3 position)
        {
            var block = grid.GetElementAt(position.x, position.z);
            return block is TowerBlock ? null : block;
        }
    }
}