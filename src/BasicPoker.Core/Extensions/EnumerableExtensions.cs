namespace BasicPoker.Core.Extensions;

public static class EnumerableExtensions
{
    private static readonly Random _rng = new();
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.Shuffle(_rng);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (rng == null) throw new ArgumentNullException(nameof(rng));

        return source.ShuffleIterator(rng);
    }

    private static IEnumerable<T> ShuffleIterator<T>(
        this IEnumerable<T> source, Random rng)
    {
        var buffer = source.ToList();
        for (int i = 0; i < buffer.Count; i++)
        {
            int j = rng.Next(i, buffer.Count);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
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
