using System;
using System.Collections.Generic;
using TowerDefenseGame.GameEntity;
using Object = UnityEngine.Object;

namespace TowerDefenseGame.Spawner
{
    public class TowerPooling : IPool<AbstractTower>
    {
        private readonly AbstractTower _towerPrefab;

        private readonly List<AbstractTower> _towerPool;

        public TowerPooling(AbstractTower towerPrefab)
        {
            _towerPool = new List<AbstractTower>();
            _towerPrefab = towerPrefab;
        }

        public AbstractTower Request()
        {
            foreach (var towerPool in _towerPool)
            {
                if (towerPool.gameObject.activeSelf) continue;
                towerPool.gameObject.SetActive(true);
                return towerPool;
            }

            var tower = Object.Instantiate(_towerPrefab);
            _towerPool.Add(tower);
            return tower;
        }

        public void Return(AbstractTower tower)
        {
            tower.gameObject.SetActive(false);
        }
    }
}