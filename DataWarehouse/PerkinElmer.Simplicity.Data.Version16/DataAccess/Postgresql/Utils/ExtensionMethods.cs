using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Utils
{
    public static class ExtensionMethods
    {
        private static int Count<TSource>(this IEnumerable<TSource> source, int count, out List<TSource> tempOutput)
        {
            ICollection<TSource> c = source as ICollection<TSource>;

            if (c != null)
            {
                tempOutput = null;
                return c.Count;
            }
            else
            {
                if (count != 0)
                    tempOutput = new List<TSource>(count);
                else
                    tempOutput = new List < TSource >();

                int result = 0;
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        tempOutput.Add(enumerator.Current);
                        result++;
                    }
                }
                return result;
            }
        }

        public static TSource[] ToQuickArray<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (count < 0) throw new ArgumentOutOfRangeException("count");

            List<TSource> tempOutput;// = new ArrayList();

            //if (count == 0)
            //{
                int updatedCount = source.Count(count, out tempOutput);
                TSource[] array = new TSource[updatedCount];
                if (tempOutput == null)
                {
                    int i = 0;
                    foreach (TSource item in source)
                    {
                        array[i++] = item;
                    }
                }
                else
                {                    
                    for (int i = 0; i < updatedCount; i++)
                    {
                        array[i] = tempOutput[i];
                    }
                }
                return array;
            //}
            //else
            //{
            //    TSource[] array = new TSource[count];
            //    int i = 0;
            //    foreach (var item in source)
            //    {
            //        array[i++] = item;
            //    }
            //    return array;
            //}
        }

        public static List<TSource> ToQuickList<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (count < 0) throw new ArgumentOutOfRangeException("count");

            List<TSource> tempOutput; //= new ArrayList();

            //if (count == 0)
            //{
                int updatedCount = source.Count(count, out tempOutput);
                List<TSource> list = new List<TSource>(updatedCount);

                if (tempOutput == null)
                {
                    foreach (TSource item in source)
                    {
                        list.Add(item);
                    }
                }
                else
                {
                    for (int i = 0; i < updatedCount; i++)
                    {
                        list.Add(tempOutput[i]);
                    }
                }                
                return list;
            //}
            //else
            //{
            //    List<TSource> list = new List<TSource>(count);
            //    foreach (var item in source)
            //    {
            //        list.Add(item);
            //    }
            //    return list;
            //}
        }
    }
}
