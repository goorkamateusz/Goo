using System.Collections.Generic;
using UnityEngine;
using Goo.Tools.Patterns;

namespace Goo.Tools.Pooling
{
    // idea private singleton ??? wih private This instead of Instance
    public class AutoObjectPooler : Singleton<AutoObjectPooler>
    {
        private class PoolingSet
        {
            public readonly PoolerList List = new PoolerList();
            public readonly Transform Parent;

            public PoolingSet()
            {
                Parent = null;
            }

            public PoolingSet(GameObject prefab)
            {
                Parent = new GameObject($"[AutoPooling] {prefab.name}").transform;
            }
        }

        private readonly Dictionary<GameObject, PoolingSet> _lists = new Dictionary<GameObject, PoolingSet>();

        public static T GetObject<T>(GameObject prefab)
        {
            return GetObject(prefab).GetComponent<T>();
        }

        public static GameObject GetObject(GameObject prefab, bool dontCreatePoolerParent = false)
        {
            PoolingSet set;
            var data = Instance._lists;

            if (!data.TryGetValue(prefab, out set))
            {
                set = dontCreatePoolerParent ? new PoolingSet() : new PoolingSet(prefab);
                data[prefab] = set;
            }

            GameObject obj = set.List.GetFree();

            if (obj == null)
            {
                obj = GameObject.Instantiate(prefab, set.Parent);
                set.List.Add(obj);
            }

            return obj;
        }
    }
}
