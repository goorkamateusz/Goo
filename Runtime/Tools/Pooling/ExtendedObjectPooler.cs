using UnityEngine;

namespace Goo.Tools.Pooling
{
    public interface IPooled
    {
        bool Recycled { get; set; }
        GameObject gameObject { get; }
    }


    public class Pooled : MonoBehaviour, IPooled
    {
        public bool Recycled { get; set; }
    }

    /// Component-based object pooler running on IPooled interface.
    /// Running `IPooled.Recycled = false` or `IPooled.DeactivateAndFree()` frees the object.
    public class ExtendedObjectPooler : ObjectPoolerBase, IObjectPooler
    {
        private class ListOfIPooled : ListOfRecyclable<IPooled>
        {
            protected override bool IsRecycled(IPooled pooled)
            {
                return pooled.Recycled;
            }
        }

        private readonly ListOfIPooled _list = new ListOfIPooled();

        public GameObject GetObject()
        {
            IPooled obj = _list.GetRecycled();
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