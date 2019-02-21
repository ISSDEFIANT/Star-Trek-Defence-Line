using System;
using System.Collections.Generic;
using System.Linq;
using Model.Ability;

namespace Model.Ship.Impl
{
    public delegate IEnumerable<KeyValuePair<string, uint>> DamageTargetManagerDelegate(string target);

    public sealed class ShipHealthAbility : IModularHealthAbility
    {
        private static readonly Random Random = new Random();
        private const string HealthStoreParameter = "shiphealthability.";

        private readonly int baseDefaultHealth;
        private readonly int baseMaxHealth;

        /// <summary>
        /// default:max
        /// </summary>
        private readonly IEnumerable<KeyValuePair<string, KeyValuePair<uint, uint>>> modules;

        private readonly DamageTargetManagerDelegate damageTargetManagerDelegate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseDefaultHealth"></param>
        /// <param name="baseMaxHealth"></param>
        /// <param name="modules">default:max</param>
        /// <param name="damageTargetManagerDelegate"></param>
        public ShipHealthAbility(int baseDefaultHealth, int baseMaxHealth,
            IEnumerable<KeyValuePair<string, KeyValuePair<uint, uint>>> modules,
            DamageTargetManagerDelegate damageTargetManagerDelegate)
        {
            this.baseDefaultHealth = baseDefaultHealth;
            this.baseMaxHealth = baseMaxHealth;
            this.modules = modules;
            this.damageTargetManagerDelegate = damageTargetManagerDelegate;
        }

        public void Init(IWorld world, IWorldObject obj)
        {
            if (baseDefaultHealth != -1 && baseMaxHealth != -1)
                obj.GetStore().Set<uint>(HealthStoreParameter + "base", baseDefaultHealth);
            foreach (var module in modules)
                obj.GetStore().Set<uint>(HealthStoreParameter + module.Key, module.Value.Key);
        }

        public void Destroy(IWorld world, IWorldObject obj)
        {
        }

        public int Damage(IWorld world, IWorldObject target, IWorldObject invoker, uint damage)
        {
            bool killed = false;
            uint realDamage = 0;
            int last = 100;
            uint currentHealth = 0;
            if (baseDefaultHealth != -1 && baseMaxHealth != -1)
            {
                var health = target.GetStore().Get<uint>(HealthStoreParameter + "base");
                if (health <= 0)
                    return -1;
                var damagePercent = Random.Next(10, last);
                last -= damagePercent;
                var dmg = (uint) (damage * 0.01F * damagePercent);
                var realDmg = Math.Min(dmg, health);
                target.GetStore().Set<uint>(HealthStoreParameter + "base", health - realDmg);
                currentHealth += health - realDmg;

                if (realDmg == health)
                    killed = true;
                foreach (var del in target.GetEventListenersForType<BaseDamagedDelegate>()
                    .Union(world.GetGlobalEventListenersForType<BaseDamagedDelegate>()))
                    del.DynamicInvoke(world, target, invoker, realDmg, health - realDmg);
                realDamage += realDmg;
            }

            foreach (var module in modules)
            {
                var damagePercent = Random.Next(last);
                if (damagePercent <= 0)
                    continue;

                last -= damagePercent;
                var dmg = (uint) (damage * 0.01F * damagePercent);
                var health = target.GetStore().Get<uint>(HealthStoreParameter + module.Key);
                var realDmg = Math.Min(dmg, health);
                target.GetStore().Set<uint>(HealthStoreParameter + module.Key, health - realDmg);
                currentHealth += health - realDmg;

                foreach (var del in target.GetEventListenersForType<ModuleDamagedDelegate>()
                    .Union(world.GetGlobalEventListenersForType<ModuleDamagedDelegate>()))
                    del.DynamicInvoke(world, target, invoker, module.Key, realDmg, health - realDmg);
                realDamage += realDmg;

                if (realDmg != health) continue;
                foreach (var del in target.GetEventListenersForType<ModuleKilledDelegate>()
                    .Union(world.GetGlobalEventListenersForType<ModuleKilledDelegate>()))
                    del.DynamicInvoke(world, target, module.Key);
            }

            foreach (var del in target.GetEventListenersForType<DamagedDelegate>()
                .Union(world.GetGlobalEventListenersForType<DamagedDelegate>()))
                del.DynamicInvoke(world, target, invoker, realDamage, currentHealth);
            if (killed || currentHealth == 0)
                foreach (var del in target.GetEventListenersForType<KilledDelegate>()
                    .Union(world.GetGlobalEventListenersForType<KilledDelegate>()))
                    del.DynamicInvoke(world, target);
            return (int) realDamage;
        }

        public int Heal(IWorld world, IWorldObject target, IWorldObject invoker, uint heal)
        {
            uint realHealForAll = 0;
            int last = 100;
            uint currentHealth = 0;
            if (baseDefaultHealth != -1 && baseMaxHealth != -1)
            {
                var health = target.GetStore().Get<uint>(HealthStoreParameter + "base");
                if (health <= 0)
                    return -1;
                var healthRequired = baseMaxHealth - health;
                int percentage = (int) (healthRequired * 1.0F / heal * 100);
                var healPercent = Random.Next(Math.Min(percentage, last));
                if (healPercent > 0)
                {
                    last -= healPercent;
                    var h = (uint) (heal * 0.01F * healPercent);
                    var realHeal = Math.Min(h, health);
                    target.GetStore().Set<uint>(HealthStoreParameter + "base", health + realHeal);
                    currentHealth += health + realHeal;

                    foreach (var del in target.GetEventListenersForType<BaseHealedDelegate>()
                        .Union(world.GetGlobalEventListenersForType<BaseHealedDelegate>()))
                        del.DynamicInvoke(world, target, invoker, realHeal, health + realHeal);
                    realHealForAll += realHeal;
                }
            }

            foreach (var module in modules)
            {
                var health = target.GetStore().Get<uint>(HealthStoreParameter + module.Key);
                var healthRequired = module.Value.Value - health;
                int percentage = (int) (healthRequired * 1.0F / heal * 100);
                var healPercent = Random.Next(Math.Min(percentage, last));
                if (healPercent > 0)
                {
                    last -= healPercent;
                    var h = (uint) (heal * 0.01F * healPercent);
                    var realHeal = Math.Min(h, healthRequired);
                    target.GetStore().Set<uint>(HealthStoreParameter + module.Key, health + realHeal);
                    currentHealth += health + realHeal;

                    foreach (var del in target.GetEventListenersForType<ModuleHealedDelegate>()
                        .Union(world.GetGlobalEventListenersForType<ModuleHealedDelegate>()))
                        del.DynamicInvoke(world, target, invoker, module.Key, realHeal, health + realHeal);
                    realHealForAll += realHeal;
                }
            }

            if (realHealForAll == 0)
                return -1;

            foreach (var del in target.GetEventListenersForType<HealedDelegate>()
                .Union(world.GetGlobalEventListenersForType<HealedDelegate>()))
                del.DynamicInvoke(world, target, invoker, realHealForAll, currentHealth);
            return (int) realHealForAll;
        }

        public uint GetHealth(IWorld world, IWorldObject target)
        {
            uint health = 0;
            if (baseMaxHealth != -1 && baseDefaultHealth != -1)
                health += target.GetStore().Get<uint>(HealthStoreParameter + "base");
            return modules.Aggregate(health,
                (current, module) => current + target.GetStore().Get<uint>(HealthStoreParameter + module.Key));
        }

        public uint GetMaxHealth(IWorld world, IWorldObject target)
        {
            return modules.Aggregate((uint) (baseMaxHealth == -1 ? 0 : baseMaxHealth),
                (current, module) => current + module.Value.Value);
        }

        public bool Kill(IWorld world, IWorldObject target)
        {
            if (baseMaxHealth != -1 && baseDefaultHealth != -1)
            {
                if (target.GetStore().Get<uint>(HealthStoreParameter + "base") == 0)
                    return false;
                target.GetStore().Set<uint>(HealthStoreParameter + "base", 0);
            }

            var lashHealth = modules.Aggregate<KeyValuePair<string, KeyValuePair<uint, uint>>, uint>(0,
                (current, module) =>
                    current + target.GetStore().Set<uint>(HealthStoreParameter + module.Key, 0));
            return lashHealth > 0;
        }

        public int GetModuleHealth(IWorld world, IWorldObject target, string name)
        {
            foreach (var module in modules)
                if (module.Key == name)
                    return (int) target.GetStore().Get<uint>(HealthStoreParameter + module.Key);

            return -1;
        }

        public int GetMaxModuleHealth(IWorld world, IWorldObject target, string name)
        {
            foreach (var module in modules)
                if (module.Key == name)
                    return (int) module.Value.Value;

            return -1;
        }

        public IEnumerable<string> GetModules(IWorld world, IWorldObject target)
        {
            return from module in modules select module.Key;
        }

        public int DamageModule(IWorld world, IWorldObject target, IWorldObject invoker, string name, uint damage)
        {
            var ret = -1;
            foreach (var module in modules)
                if (module.Key == name)
                {
                    var health = target.GetStore().Get<uint>(HealthStoreParameter + module.Key);
                    if (health == 0)
                        return 1;
                    var realDmg = Math.Min(damage, health);
                    target.GetStore().Set<uint>(HealthStoreParameter + module.Key, health - realDmg);

                    foreach (var del in target.GetEventListenersForType<ModuleDamagedDelegate>()
                        .Union(world.GetGlobalEventListenersForType<ModuleDamagedDelegate>()))
                        del.DynamicInvoke(world, target, invoker, module.Key, realDmg, health - realDmg);

                    if (realDmg != health) continue;
                    foreach (var del in target.GetEventListenersForType<ModuleKilledDelegate>()
                        .Union(world.GetGlobalEventListenersForType<ModuleKilledDelegate>()))
                        del.DynamicInvoke(world, target, module.Key);
                    ret = (int) realDmg;
                    break;
                }

            if (baseMaxHealth == -1 && baseDefaultHealth == -1)
            {
                bool dead = modules.All(module => target.GetStore().Get<uint>(HealthStoreParameter + module.Key) <= 0);
                if (dead)
                    foreach (var del in target.GetEventListenersForType<KilledDelegate>()
                        .Union(world.GetGlobalEventListenersForType<KilledDelegate>()))
                        del.DynamicInvoke(world, target);
            }

            return ret;
        }

        public int HealModule(IWorld world, IWorldObject target, IWorldObject invoker, string name, uint heal)
        {
            foreach (var module in modules)
                if (module.Key == name)
                {
                    var health = target.GetStore().Get<uint>(HealthStoreParameter + module.Key);
                    if (health == module.Value.Value)
                        return -1;
                    var healthRequired = module.Value.Value - health;

                    var realHeal = Math.Min(heal, healthRequired);
                    target.GetStore().Set<uint>(HealthStoreParameter + module.Key, health + realHeal);

                    foreach (var del in target.GetEventListenersForType<ModuleHealedDelegate>()
                        .Union(world.GetGlobalEventListenersForType<ModuleHealedDelegate>()))
                        del.DynamicInvoke(world, target, invoker, module.Key, realHeal, health + realHeal);
                    return (int) realHeal;
                }

            return -1;
        }

        public int GetBaseHealth(IWorld world, IWorldObject target)
        {
            return baseMaxHealth != -1 && baseDefaultHealth != -1
                ? (int) target.GetStore().Get<uint>(HealthStoreParameter + "base")
                : -1;
        }

        public int GetBaseMaxHealth(IWorld world, IWorldObject target)
        {
            return baseMaxHealth;
        }

        public int DamageBase(IWorld world, IWorldObject target, IWorldObject invoker, uint damage)
        {
            if (baseMaxHealth == -1 || baseDefaultHealth == -1)
                return -1;

            var health = target.GetStore().Get<uint>(HealthStoreParameter + "base");
            if (health == 0)
                return 1;
            var realDmg = Math.Min(damage, health);
            target.GetStore().Set<uint>(HealthStoreParameter + "base", health - realDmg);

            foreach (var del in target.GetEventListenersForType<BaseDamagedDelegate>()
                .Union(world.GetGlobalEventListenersForType<BaseDamagedDelegate>()))
                del.DynamicInvoke(world, target, invoker, realDmg, health - realDmg);

            if (realDmg == health)
                foreach (var del in target.GetEventListenersForType<KilledDelegate>()
                    .Union(world.GetGlobalEventListenersForType<KilledDelegate>()))
                    del.DynamicInvoke(world, target);
            return (int) realDmg;
        }

        public int HealBase(IWorld world, IWorldObject target, IWorldObject invoker, uint heal)
        {
            if (baseMaxHealth == -1 || baseDefaultHealth == -1)
                return -1;

            var health = target.GetStore().Get<uint>(HealthStoreParameter + "base");
            if (health <= 0)
                return -1;

            var healthRequired = baseMaxHealth - health;
            if (healthRequired == 0)
                return -1;

            var realHeal = Math.Min(heal, healthRequired);
            target.GetStore().Set<uint>(HealthStoreParameter + "base", health + realHeal);

            foreach (var del in target.GetEventListenersForType<BaseHealedDelegate>()
                .Union(world.GetGlobalEventListenersForType<BaseHealedDelegate>()))
                del.DynamicInvoke(world, target, invoker, realHeal, health + realHeal);
            return (int) realHeal;
        }

        public int DamageTargeted(IWorld world, IWorldObject target, IWorldObject invoker, string name, uint damage)
        {
            return (from module in damageTargetManagerDelegate(name)
                let dmg = damage * 0.01F * module.Value
                select DamageModule(world, target, invoker, module.Key, (uint) dmg)).Where(i => i > 0).Sum();
        }
    }
}