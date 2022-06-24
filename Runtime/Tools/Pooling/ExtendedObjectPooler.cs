using System.Collections.Generic;
using UnityEngine;

namespace Goo.Tools.Pooling
{
    public interface IPooled
    {
        bool IsDisabled { get; set; }
        GameObject gameObject { get; }
    }

    public class Pooled : MonoBehaviour, IPooled
    {
        public bool IsDisabled { get; set; }
    }

    public class ExtendedObjectPooler : ObjectPoolerBase, IObjectPooler
    {
        private readonly List<IPooled> _list = new List<IPooled>();

        public GameObject GetObject()
        {
            IPooled obj = null;

            foreach (var o in _list)
            {
                if (o.IsDisabled)
                {
                    obj = o;
                    break;
                }
            }

            if (obj == null)
            {
                obj = CreateNewObject();
            }

            obj.ActivateAndLock();
            return obj.gameObject;
        }

        private IPooled CreateNewObject()
        {
            GameObject go = Instantiate(_prefab, _parent);
            IPooled obj = go.GetComponent<IPooled>();

            if (obj == null)
            {
                obj = go.AddComponent<Pooled>();
            }

            _list.Add(obj);
            return obj;
        }

        public GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            var go = GetObject();
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }
    }
}