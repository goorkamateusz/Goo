using UnityEngine;

namespace Goo.Tools.Effects.Inertia
{
    public class RotationInertia : InertiaTemplate<Quaternion>
    {
        protected override Quaternion Value { get => transform.rotation; set => transform.rotation = value; }
        protected override Quaternion ParentValue => transform.parent.rotation;

        protected override Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            return Quaternion.Lerp(a, b, t);
        }
    }
}