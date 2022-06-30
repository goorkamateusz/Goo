using UnityEngine;

namespace Goo.Tools.Pooling
{
    internal class ListOfGameObject : ListOfRecyclable<GameObject>
    {
        protected override bool IsRecycled(GameObject gameObject)
        {
            return !gameObject.activeSelf;
        }
    }

    /// Component-based object pooler running on GameObjects class.
    /// Running `GameObject.SetActive(false)` frees the object.
    public class SimpleObjectPooler : ObjectPoolerBase, IObjectPooler
    {
        private readonly ListOfGameObject _list = new ListOfGameObject();

        public virtual GameObject GetObject()
        {
            GameObject obj = _list.GetRecycled();
            if (obj == null)
            {
                obj = CreateNewObject();
            }

            obj.SetActive(true);
            return obj;
        }

        private GameObject CreateNewObject()
        {
            GameObject obj = Instantiate(_prefab, _parent);
            _list.Add(obj);
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