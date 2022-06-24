using System.Collections.Generic;
using UnityEngine;

namespace Goo.Tools.Pooling
{
    public class ObjectPooler : ObjectPoolerBase, IObjectPooler
    {
        private readonly List<GameObject> _list = new List<GameObject>();

        public virtual GameObject GetObject()
        {
            GameObject obj = null;
            foreach (var o in _list)
            {
                if (!o.activeSelf)
                {
                    obj = o;
                    break;
                }
            }

            if (obj == null)
            {
                obj = Instantiate(_prefab, _parent);
                _list.Add(obj);
            }

            obj.SetActive(true);
            return obj;
        }

        public virtual GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            var obj = GetObject();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        public virtual T GetObject<T>()
        {
            GameObject obj = GetObject();
            return obj.GetComponent<T>();
        }

        public virtual void DisableAll()
        {
            foreach (var item in _list)
                item.SetActive(false);
        }

        public virtual void DestroyAll()
        {
            foreach (var item in _list)
                Destroy(item);
        }
    }
}