using System;

namespace Goo.EditorTools
{
    public class LabledListAttribute : UnityEngine.PropertyAttribute
    {
        public Type Type { get; }

        public LabledListAttribute(Type type)
        {
            Type = type;
        }
    }
}