using System.Collections.Generic;
using TowerDefenseGame.GameEntity;

namespace TowerDefenseGame.Spawner
{
    public class MonsterSpawner
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
    }
}