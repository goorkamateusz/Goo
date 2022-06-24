using Goo.Tools.EventSystem;
using NUnit.Framework;

namespace Goo.Tests.Editor.EventSystem
{
    public class EventSystemTests
    {
        class Event : IEvent<Event> { }

        class Listener : IEventListener<Event>
        {
            public Event Received { get; private set; }

            public void OnEvent(Event e) => Received = e;
        }

        [TearDown]
        public void CleanUp()
        {
            Events.ResetEventManager();
        }

        [Test]
        public void ListenerInitialState()
        {
            var listener = new Listener();
            Assert.IsFalse(listener.IsEventSubscribed());
        }

        [Test]
        public void ListenerIsSubscribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent();
            Assert.IsTrue(listener.IsEventSubscribed());
        }

        [Test]
        public void ListenerUnSubscribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent();
            listener.UnsubscribeEvent();
            Assert.IsFalse(listener.IsEventSubscribed());
        }

        [Test]
        public void ListenerGetSubscribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent();
            var @event = new Event();
            @event.Send();
            Assert.AreSame(@event, listener.Received);
        }
    }
}