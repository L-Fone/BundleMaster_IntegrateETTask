using System.Collections.Generic;

namespace ET
{
    public static class CoroutineLockComponent
    {
        /// <summary>
        /// 协程锁类型以及对应的协程锁队列
        /// </summary>
        private static readonly Dictionary<int, CoroutineLockQueue> CoroutineLockTypeToQueue = new Dictionary<int, CoroutineLockQueue>();
        
        /// <summary>
        /// 没有用到的CoroutineLock池
        /// </summary>
        internal static readonly Queue<CoroutineLock> CoroutineLockQueue = new Queue<CoroutineLock>();

        public static async ETTask<CoroutineLock> Wait(int coroutineLockType, long key)
        {
            if (!CoroutineLockTypeToQueue.TryGetValue(coroutineLockType, out CoroutineLockQueue coroutineLockQueue))
            {
                coroutineLockQueue = new CoroutineLockQueue(coroutineLockType);
                CoroutineLockTypeToQueue.Add(coroutineLockType, coroutineLockQueue);
            }
            //取一个 CoroutineLock
            CoroutineLock coroutineLock = coroutineLockQueue.GetCoroutineLock(key);
            await coroutineLock.Wait();
            return coroutineLock;
        }
    }
}

