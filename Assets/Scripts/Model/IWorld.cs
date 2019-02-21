using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Model
{
    public interface IWorld
    {
        IDataStore GetGlobalStore();
        IEnumerable<Delegate> GetGlobalEventListeners();
        IEnumerable<Delegate> GetGlobalEventListenersForType<T>();
    }
}