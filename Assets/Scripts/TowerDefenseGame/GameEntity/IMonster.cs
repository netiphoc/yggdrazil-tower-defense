namespace TowerDefenseGame.GameEntity
{
    public interface IMonster : ILivingEntity
    {
    }

    public class Monster : LivingEntity, IMonster
    {
    }
}