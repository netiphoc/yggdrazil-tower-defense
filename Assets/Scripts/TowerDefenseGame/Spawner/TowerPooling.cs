using System;
using TowerDefenseGame.GameEntity;

namespace TowerDefenseGame.Spawner
{
    public class TowerPooling : IPool<AbstractTower>
    {
        public AbstractTower Request()
        {
            return null;
        }

        public void Return(AbstractTower tower)
        {
        }
    }
}