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
            var explodeBlocks = FindExplodeBlock(targetPosition);
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

        private Block[] FindExplodeBlock(Vector3 explodePosition)
        {
            var gameManager = FindObjectOfType<GameManager>();
            var grid = gameManager.MapManager.Grid;

            return new[]
            {
                // Center
                GetBlock(grid, new Vector3(0, 0, 0) + explodePosition),
                // Front
                GetBlock(grid, new Vector3(0, 0, dealDamageArea) + explodePosition),
                // Right
                GetBlock(grid, new Vector3(dealDamageArea, 0, 0) + explodePosition),
                // Back
                GetBlock(grid, new Vector3(0, 0, -dealDamageArea) + explodePosition),
                // Left
                GetBlock(grid, new Vector3(-dealDamageArea, 0, 0) + explodePosition),
            };
        }

        private Block GetBlock(Grid<Block> grid, Vector3 position)
        {
            var block = grid.GetElementAt(position.x, position.z);
            return block is TowerBlock ? null : block;
        }
    }
}