using UnityEngine;

namespace Goo.Tools.Effects.Inertia
{
    public class PositionInertia : InertiaTemplate<Vector3>
    {
        protected override Vector3 Value { get => transform.position; set => transform.position = value; }
        protected override Vector3 ParentValue => transform.parent.position;

        protected override Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            return Vector3.Lerp(a, b, t);
        }
    }
}