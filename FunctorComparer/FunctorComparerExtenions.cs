using System;
using System.Collections.Generic;

namespace FunctorComparer
{
    public static class FunctorComparerExtenions
    {
        public static FunctorComparer<T> AsComparer<T, TKey>(this Func<T, TKey> keySelector, Comparer<TKey> comparer)
        {
            if (comparer == null) comparer = Comparer<TKey>.Default;

            int comparison(T x, T y)
            {
                return comparer.Compare(keySelector(x), keySelector(y));
            }

            return new FunctorComparer<T>(comparison);
        }

        public static FunctorComparer<T> AsComparer<T, TKey>(this Func<T, TKey> keySelector)
            => keySelector.AsComparer(null);

        public static FunctorComparer<T> AsComparer<T>(this Comparison<T> comparison)
            => new FunctorComparer<T>(comparison);
    }
}
