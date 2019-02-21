namespace Model
{
    public interface IInstanceFactory<in T>
    {
        WorldObject Create(IWorld world, T type);
    }
}