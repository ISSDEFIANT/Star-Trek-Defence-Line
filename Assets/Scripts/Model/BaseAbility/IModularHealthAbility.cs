using System.Collections.Generic;

namespace Model.Ability
{
    public delegate void ModuleDamagedDelegate(IWorld world, WorldObject target, WorldObject invoker, string name, uint damage,
        uint health);

    public delegate void ModuleHealedDelegate(IWorld world, WorldObject target, WorldObject invoker, string name, uint heal, uint health);

    public delegate void ModuleKilledDelegate(IWorld world, WorldObject target, string name);
    
    public delegate void BaseDamagedDelegate(IWorld world, WorldObject target, WorldObject invoker, uint damage, uint health);

    public delegate void BaseHealedDelegate(IWorld world, WorldObject target, WorldObject invoker, uint heal, uint health);

    /// <summary>
    /// This ability gives object a complex module health
    /// </summary>
    public interface IModularHealthAbility : IHealthAbility
    {
        /// <summary>
        /// Return health of a module
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">from who the module must be taken</param>
        /// <param name="name">the module name</param>
        /// <returns>-1 - module not found, 0 - module is dead, otherwise module's health</returns>
        int GetModuleHealth(IWorld world, WorldObject target, string name);
        /// <summary>
        /// Return module's max health
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">from who the module must be taken</param>
        /// <param name="name">the module name</param>
        /// <returns>module's max health, -1 - module not found</returns>
        int GetMaxModuleHealth(IWorld world, WorldObject target, string name);
        /// <summary>
        /// Return all module names
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target</param>
        /// <returns>all module names</returns>
        IEnumerable<string> GetModules(IWorld world, WorldObject target);
        /// <summary>
        /// Damage the module
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target object</param>
        /// <param name="invoker">who is damaging the module</param>
        /// <param name="name">module name</param>
        /// <param name="damage">how much to damage</param>
        /// <returns>-1 - module not found, otherwise module real damage</returns>
        int DamageModule(IWorld world, WorldObject target, WorldObject invoker, string name, uint damage);
        /// <summary>
        /// Target the damage to the module. Real damage will be calculated based on implementation algorithm
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target object</param>
        /// <param name="invoker">who is damaging the module</param>
        /// <param name="name">module name</param>
        /// <param name="damage">how much to damage</param>
        /// <returns>-1 - module not found, otherwise module real damage</returns>
        int DamageTargeted(IWorld world, WorldObject target, WorldObject invoker, string name, uint damage);
        /// <summary>
        /// Heal the module
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target object</param>
        /// <param name="invoker">who is healing the module</param>
        /// <param name="name">module name</param>
        /// <param name="damage">how much to heal</param>
        /// <returns>-1 - module not found, otherwise module real heal value</returns>
        int HealModule(IWorld world, WorldObject target, WorldObject invoker, string name, uint damage);
        /// <summary>
        /// Return base health
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target object</param>
        /// <returns>-1 - doesn't have a base, 0 - base/object is dead, otherwise base health value</returns>
        int GetBaseHealth(IWorld world, WorldObject target);
        /// <summary>
        /// Return base max health
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target object</param>
        /// <returns>-1 - doesn't have a base, otherwise base max health value</returns>
        int GetBaseMaxHealth(IWorld world, WorldObject target);
        /// <summary>
        /// Damage base
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target object</param>
        /// <param name="invoker">who is damaging the object</param>
        /// <param name="damage">how much to damage</param>
        /// <returns>-1 - doesn't have a base, otherwise real damage value</returns>
        int DamageBase(IWorld world, WorldObject target, WorldObject invoker, uint damage);
        /// <summary>
        /// Heal base
        /// </summary>
        /// <param name="world">the world instance</param>
        /// <param name="target">the target object</param>
        /// <param name="invoker">who is healing the object</param>
        /// <param name="damage">how much to heal</param>
        /// <returns>-1 - doesn't have a base, otherwise real heal value</returns>
        int HealBase(IWorld world, WorldObject target, WorldObject invoker, uint damage);
    }
}