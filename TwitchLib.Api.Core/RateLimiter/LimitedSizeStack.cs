using System.Collections.Generic;

namespace TwitchLib.Api.Core.RateLimiter
{
    /// <summary>
    /// Limited Size Stack
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LimitedSizeStack<T>: LinkedList<T>
    {
        private readonly int _maxSize;
        public LimitedSizeStack(int maxSize)
        {
            _maxSize = maxSize;
        }

        public void Push(T item)
        {
            AddFirst(item);

            if (Count > _maxSize)
                RemoveLast();
        }
    }
}
