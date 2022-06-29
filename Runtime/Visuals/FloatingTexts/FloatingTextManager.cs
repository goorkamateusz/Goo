using System;
using System.Collections.Generic;
using UnityEngine;
using Goo.Tools.EventSystem;
using Goo.Tools.Pooling;
using Goo.Tools.UnityHelpers;

namespace Goo.Visuals.FloatingTexts
{
    public class FloatingTextManager<TLabel> : MonoBehaviour, IEventListener<IFloatingTextEvent<TLabel>>
    {
        [Serializable]
        private struct TextPoolerType
        {
            [field: SerializeField] public TLabel Label { get; private set; }
            [field: SerializeField] public GameObject Prefab { get; private set; }
        }

        [SerializeField] private TextPoolerType[] _poolerTypes;
        [SerializeField] private Transform _camera;

        private readonly Dictionary<TLabel, int> _links = new Dictionary<TLabel, int>();

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
                _links[_poolerTypes[i].Label] = i;
            }
        }

        public void OnEvent(IFloatingTextEvent<TLabel> e)
        {
            if (_links.TryGetValue(e.Label, out int index))
            {
                var pooler = _poolerTypes[index].Prefab;
                var floatingText = AutoObjectPooler.GetObject<IFloatingText>(pooler);
                floatingText.SetText(e.Message, e.Position, _camera.rotation);
                return;
            }

            this.LogError($"FloatingTextManager doesn't contain {e.Label} pooler");
        }
    }
}