using System;

namespace Goo.EditorTools
{
    public class LabeledList : UnityEngine.PropertyAttribute
    {
        public Type Type { get; }

        public LabeledList(Type type)
        {
            Type = type;
        }
    }
}