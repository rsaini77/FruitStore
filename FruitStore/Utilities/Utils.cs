using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FruitStore.Utilities
{
    public static class IsNullOrEmptyExtension
    {
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source != null)
            {
                return !source.Cast<object>().Any();
            }
            return true;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source != null)
            {
                return !source.Cast<T>().Any();
            }
            return true;
        }
    }
}