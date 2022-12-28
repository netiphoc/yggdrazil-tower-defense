using TowerDefenseGame.GameEntity;
using UnityEngine;

namespace TowerDefenseGame.Spawner
{
    [CreateAssetMenu(fileName = "New " + nameof(TowerPrefabSo), menuName = "Tower Defense/" + nameof(TowerPrefabSo),
        order = 0)]
    public class TowerPrefabSo : ScriptableObject
    {
        [SerializeField] private AbstractTower[] towers;
        public AbstractTower[] Towers => towers;
    }
}