using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctorComparer
{

    public class CombinedEqualityComparer<T> : IEqualityComparer<T>
    {
        readonly IEnumerable<IEqualityComparer<T>> _comparers;

        public CombinedEqualityComparer(IEnumerable<IEqualityComparer<T>> comparers)
        {
            if (comparers?.Any() != true)
                throw new ArgumentNullException(nameof(comparers), "Comparers need to be supplied");

            var allComparers = new List<IEqualityComparer<T>>();

            foreach (var comparer in comparers)
            {
                switch (comparer)
                {
                    case CombinedEqualityComparer<T> combined:
                        allComparers.AddRange(combined._comparers);
                        break;

                    default:
                        allComparers.Add(comparer);
                        break;
                }
            }

            _comparers = allComparers;
        }

        public CombinedEqualityComparer(params IEqualityComparer<T>[] comparers)
            : this((IEnumerable<IEqualityComparer<T>>)comparers)
        {

        }

        public bool Equals(T x, T y)
        {
            return _comparers.All(c => c.Equals(x, y));
        }

        public int GetHashCode(T obj)
        {
            unchecked
            {
                var hashCode = 974974034;

                foreach (var comparer in _comparers)
                    hashCode = (hashCode * 397) ^ comparer.GetHashCode(obj);

                return hashCode;
            }
        }
    }
}
