using UnityEngine;
using Goo.Tools.EventSystem;

namespace Goo.Visuals.FloatingTexts
{
    public interface IFloatingTextEvent<TLabel> : IEvent<IFloatingTextEvent<TLabel>>
    {
        public Vector3 Position { get; }
        public string Message { get; }
        public TLabel Label { get; }
    }
}