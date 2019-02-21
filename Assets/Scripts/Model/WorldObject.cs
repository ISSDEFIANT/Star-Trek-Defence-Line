using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Ability;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// All objects in game world must implement this interface.
    /// All world object must have their position, local store, base abilities and event listeners
    /// </summary>
    public class WorldObject
    {
        private readonly DataStore _store;
        protected readonly Dictionary<Type, IBaseAbility> _abilities = new Dictionary<Type, IBaseAbility>();
        protected readonly Dictionary<Type, List<Delegate>> _listeners = new Dictionary<Type, List<Delegate>>();

        public Vector3 Position { get; set; }

        public IDataStore Store
        {
            get { return _store; }
        }

        public WorldObject(Vector3 position)
        {
            Position = position;
            _store = new DataStore();
        }

        public IEnumerable<IBaseAbility> GetAbilities()
        {
            return _abilities.Values;
        }

        [CanBeNull]
        public IBaseAbility GetAbility<T>()
        {
            return _abilities[typeof(T)];
        }

        public IEnumerable<Delegate> GetEventListenersForType<T>()
        {
            return _listeners[typeof(T)];
        }

        protected void AddEventListener(Delegate listener)
        {
            var type = listener.GetType();
            if (!_listeners.ContainsKey(type))
                _listeners[type] = new List<Delegate>();
            _listeners[type].Add(listener);
        }

        protected void AddAbility<T>(T ability) where T : IBaseAbility
        {
            _abilities.Add(typeof(T), ability);
        }
    }

    internal class DataStore : IDataStore
    {
        private readonly Dictionary<string, object> _map = new Dictionary<string, object>();

        public T Set<T>(string key, T value)
        {
            var old = (T) _map[key];
            _map[key] = value;
            return old;
        }

        public T Get<T>(string key)
        {
            return (T) _map[key];
        }

        public bool Contains(string key)
        {
            return _map.ContainsKey(key);
        }

        public T Remove<T>(string key)
        {
            var value = (T) _map[key];
            _map.Remove(key);
            return value;
        }
    }
}