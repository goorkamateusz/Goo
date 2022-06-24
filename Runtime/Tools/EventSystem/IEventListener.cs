namespace Goo.Tools.EventSystem
{
    public interface IEventListener
    {
    }

    public interface IEventListener<T> : IEventListener
    {
        void OnEvent(T e);
    }
}