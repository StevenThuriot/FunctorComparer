using System;
using System.Collections.Generic;

namespace FunctorComparer
{
    public static class FunctorComparer
    {
        public static FunctorComparer<T> BuildComparer<T, TKey>(this T source, Func<T, TKey> keySelector, Comparer<TKey> comparer = null) 
            => keySelector.AsComparer(comparer);

        public static FunctorComparer<T> BuildComparer<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, Comparer<TKey> comparer = null) 
            => keySelector.AsComparer(comparer);

        public static FunctorComparer<IEnumerable<T>> BuildEnumerableComparer<T, TKey>(this IEnumerable<T> source, Func<IEnumerable<T>, TKey> keySelector, Comparer<TKey> comparer = null) 
            => keySelector.AsComparer(comparer);

        public static FunctorComparer<T> BuildComparison<T>(this T source, Comparison<T> comparison) 
            => new FunctorComparer<T>(comparison);

        public static FunctorComparer<T> BuildComparison<T>(this IEnumerable<T> source, Comparison<T> comparison) 
            => new FunctorComparer<T>(comparison);

        public static FunctorComparer<IEnumerable<T>> BuildEnumerableComparison<T>(this IEnumerable<T> source, Comparison<IEnumerable<T>> comparison) 
            => new FunctorComparer<IEnumerable<T>>(comparison);
    }
}
