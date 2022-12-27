namespace GameEntity
{
    public interface IDamageAble : IEntity
    {
        void Damage(float amount);
    }

    public class DamageAble : Entity, IDamageAble
    {
        public void Damage(float amount)
        {
        }
    }
}