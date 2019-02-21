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

        public void Init(IWorld world, WorldObject obj)
        {
            obj.Store.Set(HealthPropertyName, baseHealth);
        }

        public void Destroy(IWorld world, WorldObject obj)
        {
        }

        public int Damage(IWorld world, WorldObject target, WorldObject invoker, uint damage)
        {
            var health = target.Store.Get<uint>(HealthPropertyName);
            if (health <= 0)
                return -1;

            var realDamage = health < damage ? health : damage;
            var currentHealth = health - realDamage;
            target.Store.Set(HealthPropertyName, currentHealth);
            foreach (var del in target.GetEventListenersForType<DamagedDelegate>()
                .Union(world.GetGlobalEventListenersForType<DamagedDelegate>()))
                del.DynamicInvoke(world, target, invoker, realDamage, currentHealth);

            if (health <= 0)
                Kill(world, target);
            return (int) realDamage;
        }

        public int Heal(IWorld world, WorldObject target, WorldObject invoker, uint heal)
        {
            var health = target.Store.Get<uint>(HealthPropertyName);
            if (health <= 0)
                return -1;

            var realHeal = Math.Min(heal, maxHealth - health);
            if (realHeal == 0)
                return -1;
            
            var currentHealth = health + realHeal;
            target.Store.Set(HealthPropertyName, currentHealth);
            foreach (var del in target.GetEventListenersForType<HealedDelegate>()
                .Union(world.GetGlobalEventListenersForType<HealedDelegate>()))
                del.DynamicInvoke(world, target, invoker, realHeal, currentHealth);
            
            return (int) realHeal;
        }

        public uint GetHealth(IWorld world, WorldObject target)
        {
            return target.Store.Get<uint>(HealthPropertyName);
        }

        public uint GetMaxHealth(IWorld world, WorldObject target)
        {
            return maxHealth;
        }

        public bool Kill(IWorld world, WorldObject target)
        {
            if (target.Store.Set<uint>(HealthPropertyName, 0) <= 0)
                return false;
            foreach (var del in target.GetEventListenersForType<KilledDelegate>()
                .Union(world.GetGlobalEventListenersForType<KilledDelegate>()))
                del.DynamicInvoke(world, target);
            return true;
        }
    }
}