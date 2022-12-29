using System.Collections.Generic;
using Utilities;

namespace TowerDefenseGame.GameEntity
{
    public class TowerA : AbstractTower
    {
        /// <summary>
        /// Target closest enemy
        /// </summary>
        /// <param name="monsters"></param>
        public override void TryAttackTarget(List<Monster> monsters)
        {
            var targetMonster = GetClosestMonsterInRange(monsters);
            if (!targetMonster) return;
            LookAtTarget(targetMonster);
            if (!InFireRange(targetMonster)) return;
            if (!CanFire()) return;
            SetFireDelay();

            DamageMonster(targetMonster);
            this.Log($"{name} Fire!");
        }
    }
}