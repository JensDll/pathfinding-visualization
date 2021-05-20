using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class ListExtensions
    {
        public static bool AddIf<T>(this List<T> list, T item, Func<T, bool> predicate)
        {
            if (predicate(item))
            {
                list.Add(item);
                return true;
            }

            return false;
        }
    }
}
