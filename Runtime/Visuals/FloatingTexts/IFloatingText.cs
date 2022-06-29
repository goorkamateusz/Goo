using UnityEngine;

namespace Goo.Visuals.FloatingTexts
{
    public interface IFloatingText
    {
        public void SetText(string message, Vector3 position, Quaternion rotation);
    }
}
