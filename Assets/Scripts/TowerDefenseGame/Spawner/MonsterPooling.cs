using System;
using System.Collections.Generic;
using TowerDefenseGame.GameEntity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace TowerDefenseGame.Spawner
{
    public class MonsterPooling : IPool<Monster>
    {
        private readonly Monster[] _monstersPrefabs;
        private readonly List<Monster> _monsterPool;

        public MonsterPooling(Monster[] monsterPrefabs)
        {
            _monsterPool = new List<Monster>();
            _monstersPrefabs = monsterPrefabs;
        }

        public Monster Request()
        {
            foreach (var spawnedMonster in _monsterPool)
            {
                if (spawnedMonster.gameObject.activeSelf) continue;
                spawnedMonster.ResetEntity();
                spawnedMonster.gameObject.SetActive(true);
                return spawnedMonster;
            }

            var monster = Object.Instantiate(_monstersPrefabs[Random.Range(0, _monstersPrefabs.Length)]);
            _monsterPool.Add(monster);
            return monster;
        }

        public void Return(Monster monster)
        {
            monster.gameObject.SetActive(false);
        }
    }
}