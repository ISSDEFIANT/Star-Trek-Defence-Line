using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    //TODO move to engine
    public static class InstanceUtility
    {
        private static Dictionary<Type, IInstanceFactory<object>> factories =
            new Dictionary<Type, IInstanceFactory<object>>();

        public static WorldObject Instantiate<T>(IWorld world, T t)
        {
            IInstanceFactory<T> factory;
            if (factories.ContainsKey(typeof(T)))
                factory = (IInstanceFactory<T>) factories[typeof(T)];
            else
            {
                var attr = Attribute.GetCustomAttributes(typeof(T)).OfType<InstanceFactory>()
                    .Select(attribute => attribute).FirstOrDefault();
                if (attr == null)
                    throw new ArgumentException("Type " + typeof(T) +
                                                " doesn't have a factory attribute(InstanceFactory)");
                factory = (IInstanceFactory<T>) Activator.CreateInstance(attr.Factory);
                factories.Add(typeof(T), (IInstanceFactory<object>) factory);
            }

            return factory.Create(world, t);
        }
    }
}