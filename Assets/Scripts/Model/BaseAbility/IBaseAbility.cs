namespace Model.Ability
{
    /// <summary>
    /// Base abilities give world objects a way to interact with environment
    /// </summary>
    public interface IBaseAbility
    {
        /// <summary>
        /// Init ability on world object
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="obj">the object</param>
        void Init(IWorld world, IWorldObject obj);
        /// <summary>
        /// Destroy ability before object destroy
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="obj">the object</param>
        void Destroy(IWorld world, IWorldObject obj);
    }
}