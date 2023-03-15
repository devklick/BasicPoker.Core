namespace BasicPoker.Core.Extensions;

public static class EnumerableExtensions
{
    private static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random? s_local;

        public static Random ThisThreadsRandom
        {
            get { return s_local ??= new Random(unchecked(Environment.TickCount * 31 + Environment.CurrentManagedThreadId)); }
        }
    }


    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
        return list;
    }


    public static IEnumerable<T[]> Combinations<T>(this IEnumerable<IEnumerable<T>> input)
    {
        var result = new T[input.Count()];
        var indices = new int[input.Count()];
        for (int pos = 0, index = 0; ;)
        {
            for (; pos < result.Length; pos++, index = 0)
            {
                indices[pos] = index;
                result[pos] = input.ElementAt(pos).ElementAt(index);
            }
            yield return result;
            do
            {
                if (pos == 0) yield break;
                index = indices[--pos] + 1;
            }
            while (index >= input.ElementAt(pos).Count());
        }
    }
}
