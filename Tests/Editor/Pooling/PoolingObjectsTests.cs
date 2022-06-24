using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Goo.Tools.Pooling;
using NUnit.Framework;

namespace Goo.Tests.Editor.Pooling
{
    public interface IMyMonoBehaviourTest : IMonoBehaviourTest
    {
        new bool IsTestFinished { set; get; }
    }

    public class TestablePooler : ObjectPooler, IMyMonoBehaviourTest
    {
        public bool IsTestFinished { set; get; }
    }

    internal class ExampleComponent : MonoBehaviour { }

    public abstract class PoolingObjectsTests<TPooler> where TPooler : IObjectPooler, IMyMonoBehaviourTest
    {
        protected const string Name = "TestName";

        internal TPooler _pooler;
        protected GameObject _prefab;

        [SetUp]
        public abstract void SetUp();

        [TearDown]
        public virtual void TearDown()
        {
            _pooler.IsTestFinished = true;
        }

        [Test]
        public void GetObject_Base()
        {
            var actual = _pooler.GetObject();
            Assert.IsTrue(actual.activeSelf);
            Assert.AreEqual($"{Name}(Clone)", actual.name);
        }

        [Test]
        public void GetObject_PrefabWithComponents()
        {
            _prefab.AddComponent<ExampleComponent>();
            var actual = _pooler.GetObject();
            Assert.IsNotNull(actual.transform.GetComponent<ExampleComponent>());
        }

        [Test]
        public void GetObject_Reusability()
        {
            var first = _pooler.GetObject();
            FreeObject(first);
            var second = _pooler.GetObject();
            Assert.AreSame(first, second);
        }

        [Test]
        public void GetObject_NotInRoot()
        {
            var actual = _pooler.GetObject();
            Assert.NotNull(actual.transform.parent);
        }

        [Test]
        public void GetObject_SameParent()
        {
            var first = _pooler.GetObject();
            var second = _pooler.GetObject();
            Assert.AreSame(first.transform.parent, second.transform.parent);
        }

        [Test]
        public void GetObject_DefaultPosition()
        {
            Vector3 position = new Vector3(1, 2, 3);
            Quaternion rotation = Quaternion.Euler(4, 5, 6);
            var actual = _pooler.GetObject(position, rotation);
            Assert.AreEqual(position, actual.transform.position);
            Assert.AreEqual(rotation, actual.transform.rotation);
        }

        [Test]
        public void GetObject_MultipleGetsNewElements()
        {
            var history = new List<GameObject>();
            for (int i = 0; i < 5; i++)
            {
                var actual = _pooler.GetObject();
                Assert.IsFalse(history.Contains(actual), $"Id: {i}");
                history.Add(actual);
            }
        }

        [Test]
        public void GetObject_MultipleGetExistingElements()
        {
            const int Length = 5;
            var objects = new List<GameObject>(Length);

            for (int i = 0; i < Length; i++)
            {
                var actual = _pooler.GetObject();
                objects.Add(actual);
            }

            foreach (var item in objects)
            {
                FreeObject(item);
            }

            for (int i = 0; i < Length; i++)
            {
                var actual = _pooler.GetObject();
                Assert.IsTrue(objects.Contains(actual), $"Id: {i}");
            }
        }

        protected virtual void FreeObject(GameObject item)
        {
            item.SetActive(false);
        }
    }
}