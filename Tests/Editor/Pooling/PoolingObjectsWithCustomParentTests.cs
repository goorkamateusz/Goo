using UnityEngine;
using Goo.UnitTests;
using NUnit.Framework;

namespace Goo.Tests.Editor.Pooling
{
    public class PoolingObjectsWithCustomParentTests : PoolingObjectsTests<TestablePooler>
    {
        private const string ParentName = "ParentName";
        private Transform _parent;

        public override void SetUp()
        {
            _prefab = new GameObject(Name);
            _parent = (new GameObject(ParentName)).transform;
            _pooler = MonoBehaviourInitializer<TestablePooler>.Instantiate()
                .Set("_prefab", p => p.objectReferenceValue = _prefab)
                .Set("_parent", p => p.objectReferenceValue = _parent)
                .Apply()
                .RunInEditor()
                .Get();
        }

        [Test]
        public void GetObjects_ParentValidation()
        {
            var actual = _pooler.GetObject();
            Assert.IsNotNull(actual.transform.parent);
            Assert.AreSame(_parent, actual.transform.parent);
        }

        [Test]
        public void GetObjects_SameParent()
        {
            var second = _pooler.GetObject();
            var first = _pooler.GetObject();
            Assert.AreSame(first.transform.parent, second.transform.parent);
        }
    }
}