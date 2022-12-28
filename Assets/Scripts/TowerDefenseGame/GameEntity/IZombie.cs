namespace TowerDefenseGame.GameEntity
{
    public interface IZombie : IMonster
    {
    }

    public abstract class AbstractZombie : Monster, IZombie
    {
    }
}