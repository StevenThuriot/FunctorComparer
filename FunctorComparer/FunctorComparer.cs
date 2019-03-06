using System;
using System.Collections.Generic;

namespace FunctorComparer
{
    public sealed class FunctorComparer<T> : IEqualityComparer<T>, IComparer<T>
    {
        readonly Comparison<T> _comparison;

        public FunctorComparer(Comparison<T> comparison)
        {
            _comparison = comparison ?? throw new ArgumentNullException(nameof(comparison), "Functor Comparison cannot be null.");
        }

        public int Compare(T x, T y) => _comparison(x, y);

        public bool Equals(T x, T y) => _comparison(x, y) == 0;

        public int GetHashCode(T obj) => obj?.GetHashCode() ?? 0;

        //public static implicit operator FunctorComparer<T>(Comparison<T> value)
        //    => new FunctorComparer<T>(value);

        public FunctorComparer<T> AsDescending()
        {
            var ascendingComparison = _comparison;

            int comparison(T x, T y)
            {
                return -ascendingComparison(x, y);
            }

            return new FunctorComparer<T>(comparison);
        }
    }
}
