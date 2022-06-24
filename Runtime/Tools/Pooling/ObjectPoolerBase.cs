using System;
using UnityEngine;

namespace Goo.Tools.Pooling
{
    public abstract class ObjectPoolerBase : MonoBehaviour
    {
        public const string PREFIX_NAME = "[Pooling] ";

        [SerializeField] protected GameObject _prefab;
        [Tooltip("If null parent will be created automatically")]
        [SerializeField] protected Transform _parent;

        protected virtual void Awake()
        {
            if (_prefab == null)
                throw new NullReferenceException("Prefab to pool is null");

            if (_parent == null)
            {
                var parent = new GameObject($"{PREFIX_NAME}{_prefab.name}");
                _parent = parent.transform;
            }
        }
    }
}