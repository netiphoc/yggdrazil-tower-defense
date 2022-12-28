using TowerDefenseGame.GameEntity.ScriptableObjects;
using UnityEngine;

namespace TowerDefenseGame.GameEntity
{
    public interface IEntity
    {
        EntityTypeSo GetEntityType();
    }

    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private EntityTypeSo entityTypeSo;

        protected virtual void Awake()
        {
        }

        public EntityTypeSo GetEntityType()
        {
            return entityTypeSo;
        }

        public virtual void ResetEntity()
        {
            
        }
    }
}