using System;
using System.Collections.Generic;
using TowerDefenseGame.GameEntity;
using Object = UnityEngine.Object;

namespace TowerDefenseGame.Spawner
{
    public class MonsterSpawner : IDisposable
    {
        private readonly MonsterPooling _monsterPooling;

        private readonly List<Monster> _spawnedMonsters;

        public MonsterSpawner(MonsterPrefabSo monsterPrefabSo)
        {
            _spawnedMonsters = new List<Monster>();
            _monsterPooling = new MonsterPooling(monsterPrefabSo.Monsters);
        }

        public Monster SpawnRandomMonster()
        {
            var monster = _monsterPooling.Request();
            _spawnedMonsters.Add(monster);
            return monster;
        }

        public List<Monster> GetSpawnedMonster()
        {
            return _spawnedMonsters;
        }

        public void DeSpawn(Monster monster)
        {
            _spawnedMonsters.Remove(monster);
            _monsterPooling.Return(monster);
        }

        public void Dispose()
        {
            foreach (var monster in _spawnedMonsters)
            {
                monster.ResetEntity();
                Object.Destroy(monster.gameObject);
            }
        }
    }
}