namespace Goo.Tools.EventSystem
{
    public static class IEventListenerExtensions
    {
        private static EventManager EventManager => EventManager.Instance;

        public static void SubscribeEvent<T>(this IEventListener<T> listener)
        {
            EventManager.Subscribe(listener);
        }

        public static void UnsubscribeEvent<T>(this IEventListener<T> listener)
        {
            EventManager.Unsubscribe(listener);
        }

        public static bool IsEventSubscribed<T>(this IEventListener<T> listener)
        {
            return EventManager.IsSubscriber(listener);
        }
    }
}