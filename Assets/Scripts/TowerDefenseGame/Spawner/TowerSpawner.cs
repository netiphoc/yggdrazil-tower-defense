using System.Collections.Generic;
using System.Linq;
using TowerDefenseGame.GameEntity;
using TowerDefenseGame.GameEntity.ScriptableObjects;
using TowerDefenseGame.Map;

namespace TowerDefenseGame.Spawner
{
    public class TowerSpawner
    {
        private readonly Dictionary<EntityTypeSo, TowerPooling> _prefabPool;

        private readonly List<AbstractTower> _spawnedTowers;

        public TowerSpawner(TowerPrefabSo towerPrefabSo)
        {
            _spawnedTowers = new List<AbstractTower>();
            _prefabPool = new Dictionary<EntityTypeSo, TowerPooling>();

            foreach (var towerPrefab in towerPrefabSo.Towers)
            {
                if (_prefabPool.ContainsKey(towerPrefab.GetEntityType())) continue;
                _prefabPool.Add(towerPrefab.GetEntityType(), new TowerPooling(towerPrefab));
            }
        }

        public AbstractTower SpawnEntityType(EntityTypeSo entityType, Block block)
        {
            if (!_prefabPool.ContainsKey(entityType)) return null;
            var tower = _prefabPool[entityType].Request();
            tower.transform.position = block.GetWorldPosition();
            _spawnedTowers.Add(tower);
            return tower;
        }

        public List<AbstractTower> GetSpawnedTowers()
        {
            return _spawnedTowers;
        }

        public void DeSpawn(AbstractTower tower)
        {
            _spawnedTowers.Remove(tower);
            _prefabPool[tower.GetEntityType()].Return(tower);
        }

        public EntityTypeSo[] GetTowers()
        {
            return _prefabPool.Keys.ToArray();
        }
    }
}