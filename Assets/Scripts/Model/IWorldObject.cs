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
    public interface IWorldObject
    {
        Vector3 GetPosition();
        IDataStore GetStore();
        IEnumerable<IBaseAbility> GetAbilities();
        [CanBeNull] IBaseAbility GetAbility<T>();
        IEnumerable<Delegate> GetEventListeners();
        IEnumerable<Delegate> GetEventListenersForType<T>();
    }
}