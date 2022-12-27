namespace GameEntity
{
    public interface IZombie : IDamageAble
    {
    }

    public class Zombie : DamageAble, IZombie
    {
    }
}