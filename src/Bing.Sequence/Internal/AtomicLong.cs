using System;
using System.Threading;

namespace Bing.Sequence.Internal
{
    /// <summary>
    /// 原子长整型
    /// </summary>
    internal class AtomicLong
    {
        /// <summary>
        /// 值
        /// </summary>
        private long _value;

        /// <summary>
        /// 初始化一个<see cref="AtomicLong"/>类型的实例
        /// </summary>
        public AtomicLong() : this(0) { }

        /// <summary>
        /// 初始化一个<see cref="AtomicLong"/>类型的实例
        /// </summary>
        /// <param name="value">初始值</param>
        public AtomicLong(long value) => _value = value;

        /// <summary>
        /// 获取并添加指定值。先增加，后获取
        /// </summary>
        /// <param name="delta">递增值</param>
        public long AddAndGet(long delta)
        {
            for (; ; )
            {
                long current = Get();
                long next = current + delta;
                if (CompareAndSet(current, next))
                {
                    return next;
                }
            }
        }

        /// <summary>
        /// 比较并设置
        /// </summary>
        /// <param name="expect">目标值</param>
        /// <param name="update">更新值</param>
        public bool CompareAndSet(long expect, long update) => Interlocked.CompareExchange(ref _value, update, expect) == expect;

        /// <summary>
        /// 获取并减少。先减少，后获取
        /// </summary>
        public long DecrementAndGet()
        {
            for (; ; )
            {
                long current = Get();
                long next = current - 1;
                if (CompareAndSet(current, next))
                {
                    return next;
                }
            }
        }

        /// <summary>
        /// 获取当前值
        /// </summary>
        public long Get() => Interlocked.Read(ref _value);

        /// <summary>
        /// 获取并添加指定值。先获取，后增加
        /// </summary>
        /// <param name="delta">递增值</param>
        public long GetAndAdd(long delta)
        {
            for (; ; )
            {
                long current = Get();
                long next = current + delta;
                if (CompareAndSet(current, next))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// 获取并减少。先获取，后减少
        /// </summary>
        public long GetAndDecrement()
        {
            for (; ; )
            {
                long current = Get();
                long next = current - 1;
                if (CompareAndSet(current, next))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// 获取并增加。先获取，后增加
        /// </summary>
        /// <returns></returns>
        public long GetAndIncrement() => GetAndIncrement(1);

        /// <summary>
        /// 获取并添加指定值。先获取，后增加
        /// </summary>
        /// <param name="delta">递增值</param>
        public long GetAndIncrement(long delta)
        {
            for (; ; )
            {
                long current = Get();
                long next = current + delta;
                if (CompareAndSet(current, next))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// 获取并设置值，获取原有值，设置新值
        /// </summary>
        /// <param name="value">值</param>
        public long GetAndSet(long value) => Interlocked.Exchange(ref _value, value);

        /// <summary>
        /// 设置当前值
        /// </summary>
        /// <param name="value">值</param>
        public void Set(long value) => Interlocked.Exchange(ref _value, value);

        /// <summary>
        /// 重写转换字符串方法
        /// </summary>
        public override string ToString() => Convert.ToString(Get());

        /// <summary>
        /// 重写隐式转换，允许AtomicLong到long的转换
        /// </summary>
        /// <param name="value">值</param>
        public static implicit operator long(AtomicLong value) => value.Get();
    }
}
