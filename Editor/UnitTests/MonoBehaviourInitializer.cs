using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Goo.UnitTests
{
    public class MonoBehaviourInitializer<T> where T : MonoBehaviour, IMonoBehaviourTest
    {
        private T _component;
        private SerializedObject _serialized;

        private MonoBehaviourInitializer(T component)
        {
            _component = component;
            _serialized = new SerializedObject(component);
        }

        public MonoBehaviourInitializer<T> Set(string name, Action<SerializedProperty> action)
        {
            action(_serialized.FindProperty(name));
            return this;
        }

        public MonoBehaviourInitializer<T> RunInEditor()
        {
            _component.runInEditMode = true;
            return this;
        }

        public MonoBehaviourInitializer<T> Apply()
        {
            if (_serialized.hasModifiedProperties)
                _serialized.ApplyModifiedProperties();
            return this;
        }

        public T Get()
        {
            Apply();
            return _component;
        }

        public static MonoBehaviourInitializer<T> Instantiate(string name = null)
        {
            var go = new GameObject(name);
            var t = go.AddComponent<T>();
            return new MonoBehaviourInitializer<T>(t);
        }
    }
}
