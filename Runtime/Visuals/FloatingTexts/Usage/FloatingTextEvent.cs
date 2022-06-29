using UnityEngine;

namespace Goo.Visuals.FloatingTexts.Usage
{
    public struct FloatingTextEvent : IFloatingTextEvent<string>
    {
        public Vector3 Position { get; set; }
        public string Message { get; set; }
        public string Label { get; set; }
    }
}