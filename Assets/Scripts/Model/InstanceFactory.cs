using System;

namespace Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InstanceFactory : Attribute
    {
        private readonly Type factory;

        public InstanceFactory(Type factory)
        {
            this.factory = factory;
        }

        public Type Factory
        {
            get { return factory; }
        }
    }
}