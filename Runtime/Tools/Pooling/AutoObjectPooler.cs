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
            public readonly ListOfGameObject List = new ListOfGameObject();
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

        public static T GetObject<T>(GameObject prefab, bool dontCreatePoolerParent = false)
        {
            return GetObject(prefab, dontCreatePoolerParent).GetComponent<T>();
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

            GameObject obj = set.List.GetRecycled();

            if (obj == null)
            {
                obj = GameObject.Instantiate(prefab, set.Parent);
                set.List.Add(obj);
            }

            return obj;
        }
    }
}
