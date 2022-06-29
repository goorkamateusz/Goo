using System;
using System.Collections.Generic;
using UnityEngine;

namespace Goo.Tools.Pooling
{
    [Serializable]
    internal class PoolerList : List<GameObject>
    {
        public GameObject GetFree()
        {
            GameObject obj = null;
            foreach (var o in this)
            {
                if (!o.activeSelf)
                {
                    obj = o;
                    break;
                }
            }
            return obj;
        }
    }
}