using System;

namespace Utils.Singleton
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SingletonBehaviourAttribute : Attribute
    {
        public SingletonBehaviourMode BehaviourMode { get; }

        public SingletonBehaviourAttribute(SingletonBehaviourMode mode)
        {
            BehaviourMode = mode;
        }
    }
}
