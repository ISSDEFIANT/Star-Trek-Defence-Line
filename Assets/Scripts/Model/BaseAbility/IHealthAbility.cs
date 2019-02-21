namespace Model.Ability
{
    /// <summary>
    /// This listener will be invoked when subscribed object was damaged
    /// </summary>
    /// <param name="target">who was damaged</param>
    /// <param name="damage">how much real damage is dealt</param>
    /// <param name="health">how much health is left</param>
    /// <param name="world">the world instance</param>
    /// <param name="invoker">who dealt the damage</param>
    public delegate void DamagedDelegate(IWorld world, IWorldObject target, IWorldObject invoker, uint damage,
        uint health);

    /// <summary>
    /// This listener will be invoked when subscribed object was killed
    /// </summary>
    /// <param name="world">the world instance</param>
    /// <param name="target">who was killed</param>
    public delegate void KilledDelegate(IWorld world, IWorldObject target);

    /// <summary>
    /// This listener will be invoked when subscribed object was healed 
    /// </summary>
    /// <param name="world">the world instance</param>
    /// <param name="invoker">who heal the object</param>
    /// <param name="heal">real heal value</param>
    /// <param name="health">health after heal</param>
    /// <param name="target">who was healed</param>
    public delegate void HealedDelegate(IWorld world, IWorldObject target, IWorldObject invoker, uint heal,
        uint health);

    /// <summary>
    /// This ability gives object a health bar
    /// </summary>
    public interface IHealthAbility : IBaseAbility
    {
        /// <summary>
        /// Damage the object
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">who must be damaged</param>
        /// <param name="invoker">who damage the target</param>
        /// <param name="damage">how much damage is dealt</param>
        /// <returns>how much real damage is dealt to the ship. -1 will return if the ship is already dead</returns>
        int Damage(IWorld world, IWorldObject target, IWorldObject invoker, uint damage);

        /// <summary>
        /// Heal the object
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">who must be healed</param>
        /// <param name="invoker">how heal the target</param>
        /// <param name="heal">heal value</param>
        /// <returns>how much real heal was applied. -1 will return if the ship is already healed or dead</returns>
        int Heal(IWorld world, IWorldObject target, IWorldObject invoker, uint heal);

        /// <summary>
        /// Return target's health
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target</param>
        /// <returns>target's health</returns>
        uint GetHealth(IWorld world, IWorldObject target);

        /// <summary>
        /// Return target's max health
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target</param>
        /// <returns>target's max health</returns>
        uint GetMaxHealth(IWorld world, IWorldObject target);

        /// <summary>
        /// Kill the target immediately
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target</param>
        /// <returns>true - target killed, false - target is already dead</returns>
        bool Kill(IWorld world, IWorldObject target);
    }
}