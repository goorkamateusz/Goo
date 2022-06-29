using System;
using System.Collections.Generic;
using UnityEngine;
using Goo.Tools.EventSystem;
using Goo.Tools.Pooling;
using Goo.Tools.UnityHelpers;

namespace Goo.Visuals.FloatingTexts
{
    public struct FloatingTextEvent : IEvent<FloatingTextEvent>
    {
        public Vector3 Position { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }

    public class FloatingTextManager : MonoBehaviour, IEventListener<FloatingTextEvent>
    {
        [Serializable]
        private struct TextPoolerType
        {
            [field: SerializeField] public string Type { get; private set; }
            [field: SerializeField] public ObjectPooler Pooler { get; private set; }
        }

        [SerializeField] private TextPoolerType[] _poolerTypes;
        [SerializeField] private Transform _camera;

        private readonly Dictionary<string, int> _links = new Dictionary<string, int>();

        protected virtual void Awake()
        {
            if (_camera == null) _camera = Camera.main.transform;
            UpdateLinkDictionary();
        }

        protected virtual void OnEnable()
        {
            this.SubscribeEvent();
        }

        protected virtual void OnDisable()
        {
            this.UnsubscribeEvent();
        }

        private void UpdateLinkDictionary()
        {
            _links.Clear();
            for (int i = 0; i < _poolerTypes.Length; i++)
            {
                _links[_poolerTypes[i].Type] = i;
            }
        }

        public void OnEvent(FloatingTextEvent e)
        {
            if (_links.TryGetValue(e.Type, out int index))
            {
                var pooler = _poolerTypes[index].Pooler;
                var floatingText = pooler.GetObject<FloatingText>();
                floatingText.SetText(e.Message, e.Position, _camera.rotation);
                return;
            }

            this.LogError($"FloatingTextManager doesn't contain {e.Type} pooler");
        }
    }
}