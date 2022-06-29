namespace Goo.Tools.Pooling
{
    public static class IPooledHandler
    {
        public static void DeactivateAndFree(this IPooled obj)
        {
            obj.Recycled = true;
            obj.gameObject.SetActive(false);
        }

        public static void ActivateAndLock(this IPooled obj)
        {
            obj.Recycled = false;
            obj.gameObject.SetActive(true);
        }
    }
}