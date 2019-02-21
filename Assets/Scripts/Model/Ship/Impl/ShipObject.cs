using System.Collections.Generic;
using System.Linq;
using Model.Ship.Impl;
using UnityEngine;

namespace Model.Ship
{
    public class ShipObject : WorldObject
    {
        private readonly ShipTypeBase _base;
        private readonly string _name;

        public ShipTypeBase Base
        {
            get { return _base; }
        }

        public string Name
        {
            get { return _name; }
        }

        public ShipObject(Vector3 position, ShipTypeBase @base, string name) : base(position)
        {
            _base = @base;
            _name = name;
            var healthModules = from subsystem in @base.Subsystems
                where subsystem.Health >= 0
                select new KeyValuePair<string, KeyValuePair<uint, uint>>(subsystem.Name,
                    new KeyValuePair<uint, uint>((uint) subsystem.Health, (uint) subsystem.Health));
            AddAbility(new ShipHealthAbility((int) @base.Health, (int) @base.Health, healthModules, DamageTargetManager));
        }

        private static IEnumerable<KeyValuePair<string, uint>> DamageTargetManager(string module)
        {
            //todo
            return new KeyValuePair<string, uint>[0];
        }
    }
}