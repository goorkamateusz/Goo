using System.Collections.Generic;

namespace Goo.Tools.Pooling
{
    internal abstract class ListOfRecyclable<T> : List<T> where T : class
    {
        public T GetRecycled()
        {
            T obj = null;
            foreach (var o in this)
            {
                if (IsRecycled(o))
                {
                    obj = o;
                    break;
                }
            }
            return obj;
        }

        protected abstract bool IsRecycled(T o);
    }
}