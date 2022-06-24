using System.Collections.Generic;
using UnityEngine;

namespace Goo.Tools.Effects.Inertia
{
    public abstract class InertiaTemplate<T> : MonoBehaviour
    {
        [SerializeField] private float _stamplingTime = 0.2f;
        [SerializeField] private int _length = 5;

        private T _current;
        private T _last;
        private float _sampleTimer;
        // idea optimise with CircleArray 
        private Queue<T> _queue = new Queue<T>();

        protected abstract T Value { get; set; }
        protected abstract T ParentValue { get; }

        protected virtual void Start()
        {
            _last = _current = Value;
        }

        protected virtual void LateUpdate()
        {
            if (_queue.Count > _length)
            {
                _last = _current;
                _current = _queue.Dequeue();
            }

            _sampleTimer += Time.deltaTime;
            if (_sampleTimer > _stamplingTime)
            {
                _sampleTimer = 0f;
                _queue.Enqueue(ParentValue);
                Value = _current;
            }
            else
            {
                Value = Lerp(_last, _current, _sampleTimer / _stamplingTime);
            }
        }

        protected abstract T Lerp(T a, T b, float t);

        protected virtual void OnValidate()
        {
            Value = default;
        }
    }
}