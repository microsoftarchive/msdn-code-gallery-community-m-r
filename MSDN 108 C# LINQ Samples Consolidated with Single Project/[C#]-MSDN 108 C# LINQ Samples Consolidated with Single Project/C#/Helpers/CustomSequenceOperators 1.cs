using System.Collections.Generic;
using System.Data;

namespace LINQ101Samples
{
    public static class CustomSequenceOperators
    {
        public static IEnumerable<S> Combine<S>(this    IEnumerable<DataRow> first, 
                                                        IEnumerable<DataRow> second, 
                                                        System.Func<DataRow, DataRow, S> func)
        {
            using (IEnumerator<DataRow> e1 = first.GetEnumerator(), e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    yield return func(e1.Current, e2.Current);
                }
            }
        }
    }
}
