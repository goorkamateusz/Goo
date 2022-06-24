using UnityEngine;

namespace Goo.Tools.UnityHelpers
{
    public static class Axis3
    {
        // idea move to Tools
        public enum Enum
        {
            forward,
            back,
            left,
            right,
            up,
            down,
        }

        public static Vector3 Translate(Enum axis)
        {
            switch (axis)
            {
                case Enum.forward: return Vector3.forward;
                case Enum.back: return Vector3.back;
                case Enum.left: return Vector3.left;
                case Enum.right: return Vector3.right;
                case Enum.up: return Vector3.up;
                case Enum.down: return Vector3.down;
            }
            return Vector3.zero;
        }
    }
}