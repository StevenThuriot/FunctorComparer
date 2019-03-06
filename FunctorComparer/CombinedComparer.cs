using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctorComparer
{
    public class CombinedComparer<T> : IComparer<T>
    {
        readonly IReadOnlyList<IComparer<T>> _comparers;

        public CombinedComparer(IEnumerable<IComparer<T>> comparers)
        {
            if (comparers?.Any() != true)
                throw new ArgumentNullException(nameof(comparers), "Comparers need to be supplied");

            var allComparers = new List<IComparer<T>>();

            foreach (var comparer in comparers)
            {
                switch (comparer)
                {
                    case CombinedComparer<T> combined:
                        allComparers.AddRange(combined._comparers);
                        break;

                    default:
                        allComparers.Add(comparer);
                        break;
                }
            }

            _comparers = allComparers.AsReadOnly();
        }

        public CombinedComparer(params IComparer<T>[] comparers)
            : this((IEnumerable<IComparer<T>>)comparers)
        {

        }

        public int Compare(T x, T y)
        {
            for (int i = 0; i < _comparers.Count; i++)
            {
                var result = _comparers[i].Compare(x, y);

                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }
    }
}
