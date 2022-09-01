using System.Collections.Generic;

namespace SUMBER.Models.Helper
{
    public class GroupHelper<K, T>
    {
        public K Key;
        public IEnumerable<T> Values;
    }
}
