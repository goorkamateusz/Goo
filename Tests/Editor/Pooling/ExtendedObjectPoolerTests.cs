using UnityEngine;
using Goo.Tools.Pooling;
using Goo.UnitTests;
using NUnit.Framework;

namespace Goo.Tests.Editor.Pooling
{
    public class ExtendedTestablePooler : ExtendedObjectPooler, IMyMonoBehaviourTest
    {
        public bool IsTestFinished { get; set; }
    }

    internal class CustomPooled : MonoBehaviour, IPooled
    {
        public bool IsDisabled { get; set; }
    }

    public class ExtendedObjectPoolerTests : PoolingObjectsTests<ExtendedTestablePooler>
    {
        [SetUp]
        public override void SetUp()
        {
            _prefab = new GameObject("TestName");
            _pooler = MonoBehaviourInitializer<ExtendedTestablePooler>.Instantiate()
                .Set("_prefab", p => p.objectReferenceValue = _prefab)
                .Apply()
                .RunInEditor()
                .Get();
        }

        [Test]
        public void GetObject_WithoutIPooled()
        {
            var obj = _pooler.GetObject();
            IPooled pooled = obj.GetComponent<IPooled>();
            Assert.IsNotNull(pooled);
            Assert.IsFalse(pooled.IsDisabled);
            Assert.IsTrue(obj.activeSelf);
        }

        [Test]
        public void GetObject_WithPooledComponent()
        {
            _prefab.AddComponent<Pooled>();
            var obj = _pooler.GetObject();
            IPooled pooled = obj.GetComponent<IPooled>();
            Assert.IsNotNull(pooled);
            Assert.IsFalse(pooled.IsDisabled);
            Assert.IsTrue(obj.activeSelf);
        }

        [Test]
        public void GetObject_WithCustomIPooled()
        {
            _prefab.AddComponent<CustomPooled>();
            var obj = _pooler.GetObject();
            IPooled pooled = obj.GetComponent<IPooled>();
            Assert.IsNotNull(pooled);
            Assert.IsFalse(pooled.IsDisabled);
            Assert.IsTrue(obj.activeSelf);
        }

        [Test]
        public void GetObject_ReusabilityOfIPooledBehaviour()
        {
            _prefab.AddComponent<CustomPooled>();
            var first = _pooler.GetObject();
            CustomPooled pooled1 = first.GetComponent<CustomPooled>();
            pooled1.DeactivateAndFree();
            var second = _pooler.GetObject();
            IPooled pooled2 = second.GetComponent<IPooled>();
            Assert.AreSame(first, second);
            Assert.AreSame(pooled1, pooled2);
            Assert.IsFalse(pooled2.IsDisabled);
        }

        protected override void FreeObject(GameObject item)
        {
            item.GetComponent<IPooled>().IsDisabled = true;
        }
    }
}