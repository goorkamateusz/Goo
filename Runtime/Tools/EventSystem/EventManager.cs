using System;
using System.Collections.Generic;
using Goo.Tools.Patterns;

namespace Goo.Tools.EventSystem
{
    internal class EventManager : Singleton<EventManager>
    {
        private Dictionary<Type, List<IEventListener>> subscribers = new Dictionary<Type, List<IEventListener>>();

        internal void Subscribe<T>(IEventListener<T> listener)
        {
            Type eventType = typeof(T);
            List<IEventListener> list;

            if (!subscribers.TryGetValue(eventType, out list))
            {
                list = new List<IEventListener>();
                subscribers[eventType] = list;
            }

            if (!list.Contains(listener))
                list.Add(listener);
        }

        internal void Unsubscribe<T>(IEventListener<T> listener)
        {
            if (subscribers.TryGetValue(typeof(T), out var list))
                list?.Remove(listener);
        }

        internal bool IsSubscriber<T>(IEventListener<T> listener)
        {
            if (subscribers.TryGetValue(typeof(T), out var list))
                return list.Contains(listener);
            return false;
        }

        internal void Trigger<T>(T e)
        {
            if (subscribers.TryGetValue(typeof(T), out var list))
            {
                foreach (var listener in list)
                {
                    (listener as IEventListener<T>).OnEvent(e);
                }
            }
        }

        internal void NullSingleton()
        {
            __NullSingleton();
        }
    }

    public static class Events
    {
        public static void ResetEventManager()
        {
            if (EventManager.Initialized)
                EventManager.Instance.NullSingleton();
        }
    }
}