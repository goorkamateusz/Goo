using Goo.Tools.EventSystem;
using NUnit.Framework;

namespace Goo.Tests.Editor.EventSystem
{
    public class MultipleEventListenerTests
    {
        class EventA : IEvent<EventA> { }
        class EventB : IEvent<EventB> { }

        class Listener : IEventListener<EventA>, IEventListener<EventB>
        {
            public EventA ReceivedA { get; private set; }
            public EventB ReceivedB { get; private set; }

            public void OnEvent(EventA e) => ReceivedA = e;
            public void OnEvent(EventB e) => ReceivedB = e;
        }

        [TearDown]
        public void CleanUp()
        {
            Events.ResetEventManager();
        }

        [Test]
        public void ListenerInitalState()
        {
            var listener = new Listener();
            Assert.IsFalse(listener.IsEventSubscribed<EventA>());
            Assert.IsFalse(listener.IsEventSubscribed<EventB>());
        }

        [Test]
        public void ListenerIsSubscribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent<EventA>();
            listener.SubscribeEvent<EventB>();
            Assert.IsTrue(listener.IsEventSubscribed<EventA>());
            Assert.IsTrue(listener.IsEventSubscribed<EventB>());
        }
        [Test]

        public void ListenerIsSubscribedOnce()
        {
            var listener = new Listener();
            listener.SubscribeEvent<EventB>();
            Assert.IsFalse(listener.IsEventSubscribed<EventA>());
            Assert.IsTrue(listener.IsEventSubscribed<EventB>());
        }

        [Test]
        public void ListenerUnSubsribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent<EventA>();
            listener.SubscribeEvent<EventB>();
            listener.UnsubscribeEvent<EventA>();
            listener.UnsubscribeEvent<EventB>();
            Assert.IsFalse(listener.IsEventSubscribed<EventA>());
            Assert.IsFalse(listener.IsEventSubscribed<EventB>());
        }

        [Test]
        public void ListenerUnSubsribedOnce()
        {
            var listener = new Listener();
            listener.SubscribeEvent<EventA>();
            listener.SubscribeEvent<EventB>();
            listener.UnsubscribeEvent<EventB>();
            Assert.IsTrue(listener.IsEventSubscribed<EventA>());
            Assert.IsFalse(listener.IsEventSubscribed<EventB>());
        }

        [Test]
        public void ListenerGetSubsribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent<EventA>();
            listener.SubscribeEvent<EventB>();
            var eventA = new EventA();
            var eventB = new EventB();
            eventA.Send();
            eventB.Send();
            Assert.AreSame(eventA, listener.ReceivedA);
            Assert.AreSame(eventB, listener.ReceivedB);
            Assert.AreNotSame(listener.ReceivedA, listener.ReceivedB);
        }

        [Test]
        public void ListenerGetSubsribedOnce()
        {
            var listener = new Listener();
            listener.SubscribeEvent<EventA>();
            listener.SubscribeEvent<EventB>();
            var eventB = new EventB();
            eventB.Send();
            Assert.IsNull(listener.ReceivedA);
            Assert.AreSame(eventB, listener.ReceivedB);
        }
    }
}