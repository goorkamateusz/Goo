using UnityEngine;

namespace Goo.Tools.UnityHelpers
{
    public static class GameObjectHelper
    {
        public static GameObject AddChild(this Transform transform, string name = "GameObject")
        {
            var child = new GameObject(name);
            child.transform.parent = transform;
            return child;
        }
    }
}
