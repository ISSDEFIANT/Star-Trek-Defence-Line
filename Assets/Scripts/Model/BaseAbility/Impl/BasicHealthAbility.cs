using System;
using System.Linq;

namespace Model.Ability.Impl
{
    /// <summary>
    /// This implementation will add basic health bar to the object
    /// </summary>
    public sealed class BasicHealthAbility : IHealthAbility
    {
        private const string HealthPropertyName = "healthability.health";

        private readonly uint baseHealth;
        private readonly uint maxHealth;

        public BasicHealthAbility(uint baseHealth, uint maxHealth)
        {
            this.baseHealth = baseHealth;
            this.maxHealth = maxHealth;
        }

        public void Init(IWorld world, IWorldObject obj)
        {
            obj.GetStore().Set<uint>(HealthPropertyName, baseHealth);
        }

        public void Destroy(IWorld world, IWorldObject obj)
        {
        }

        public int Damage(IWorld world, IWorldObject target, IWorldObject invoker, uint damage)
        {
            var health = target.GetStore().Get<uint>(HealthPropertyName);
            if (health <= 0)
                return -1;

            var realDamage = health < damage ? health : damage;
            var currentHealth = health - realDamage;
            target.GetStore().Set<uint>(HealthPropertyName, currentHealth);
            foreach (var del in target.GetEventListenersForType<DamagedDelegate>()
                .Union(world.GetGlobalEventListenersForType<DamagedDelegate>()))
                del.DynamicInvoke(world, target, invoker, realDamage, currentHealth);

            if (health <= 0)
                Kill(world, target);
            return (int) realDamage;
        }

        public int Heal(IWorld world, IWorldObject target, IWorldObject invoker, uint heal)
        {
            var health = target.GetStore().Get<uint>(HealthPropertyName);
            if (health <= 0)
                return -1;

            var realHeal = Math.Min(heal, maxHealth - health);
            if (realHeal == 0)
                return -1;
            
            var currentHealth = health + realHeal;
            target.GetStore().Set<uint>(HealthPropertyName, currentHealth);
            foreach (var del in target.GetEventListenersForType<HealedDelegate>()
                .Union(world.GetGlobalEventListenersForType<HealedDelegate>()))
                del.DynamicInvoke(world, target, invoker, realHeal, currentHealth);
            
            return (int) realHeal;
        }

        public uint GetHealth(IWorld world, IWorldObject target)
        {
            return target.GetStore().Get<uint>(HealthPropertyName);
        }

        public uint GetMaxHealth(IWorld world, IWorldObject target)
        {
            return maxHealth;
        }

        public bool Kill(IWorld world, IWorldObject target)
        {
            if (target.GetStore().Set<uint>(HealthPropertyName, 0) <= 0)
                return false;
            foreach (var del in target.GetEventListenersForType<KilledDelegate>()
                .Union(world.GetGlobalEventListenersForType<KilledDelegate>()))
                del.DynamicInvoke(world, target);
            return true;
        }
    }
}