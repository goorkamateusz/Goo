namespace Goo.Tools.EventSystem
{
    public static class IEventExtensions
    {
        public static void Send<T>(this IEvent<T> e)
        {
            EventManager.Instance.Trigger((T)e);
        }

        // idea public static void DeleteEventStream<T>(this IEvent<T> e)
    }
}