using TowerDefenseGame.GameEntity;
using UnityEngine;

namespace TowerDefenseGame.Spawner
{
    [CreateAssetMenu(fileName = "New " + nameof(MonsterPrefabSo), menuName = "Tower Defense/" + nameof(MonsterPrefabSo),
        order = 0)]
    public class MonsterPrefabSo : ScriptableObject
    {
        [SerializeField] private Monster[] monsters;
        public Monster[] Monsters => monsters;
    }
}