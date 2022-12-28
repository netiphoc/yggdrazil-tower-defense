using TowerDefenseGame.GameEntity;
using UnityEngine;

namespace TowerDefenseGame.Spawner
{
    [CreateAssetMenu(fileName = "New " + nameof(EnemyPrefabSo), menuName = "Tower Defense/" + nameof(EnemyPrefabSo),
        order = 0)]
    public class EnemyPrefabSo : ScriptableObject
    {
        [SerializeField] private Monster[] monsters;
        public Monster[] Monsters => monsters;
    }
}