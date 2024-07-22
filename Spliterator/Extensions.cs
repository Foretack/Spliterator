using System.Buffers;

namespace Spliterator;

public static class Extensions
{
    #region Spans
    public static SpanSpliterator<T> Spliterate<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separator)
    where T : IEquatable<T>? => new(span, separator);
    public static SpanSpliterator<T> SpliterateAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separator)
        where T : IEquatable<T>? => new(span, separator, SplitType.Any);
    public static SpanSpliterator<T> SpliterateAnyExcept<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> separator)
        where T : IEquatable<T>? => new(span, separator, SplitType.AnyExcept);

    public static SingleSpliterator<T> Spliterate<T>(this ReadOnlySpan<T> span, T separator)
        where T : IEquatable<T>? => new(span, separator);
    public static SingleSpliterator<T> SpliterateAnyExcept<T>(this ReadOnlySpan<T> span, T separator)
        where T : IEquatable<T>? => new(span, separator, SplitType.AnyExcept);

    public static SearchSpliterator<T> SpliterateAny<T>(this ReadOnlySpan<T> span, SearchValues<T> separator)
        where T : IEquatable<T>? => new(span, separator, SplitType.Any);
    public static SearchSpliterator<T> SpliterateAnyExcept<T>(this ReadOnlySpan<T> span, SearchValues<T> separator)
        where T : IEquatable<T>? => new(span, separator, SplitType.AnyExcept);
    #endregion

    #region Arrays
    public static SpanSpliterator<T> Spliterate<T>(this T[] arr, ReadOnlySpan<T> separator)
        where T : IEquatable<T>? => new(arr.AsSpan(), separator);
    public static SpanSpliterator<T> SpliterateAny<T>(this T[] arr, ReadOnlySpan<T> separator)
        where T : IEquatable<T>? => new(arr.AsSpan(), separator, SplitType.Any);
    public static SpanSpliterator<T> SpliterateAnyExcept<T>(this T[] arr, ReadOnlySpan<T> separator)
        where T : IEquatable<T>? => new(arr.AsSpan(), separator, SplitType.AnyExcept);

    public static SingleSpliterator<T> Spliterate<T>(this T[] arr, T separator)
        where T : IEquatable<T>? => new(arr.AsSpan(), separator);
    public static SingleSpliterator<T> SpliterateAnyExcept<T>(this T[] arr, T separator)
        where T : IEquatable<T>? => new(arr.AsSpan(), separator, SplitType.AnyExcept);

    public static SearchSpliterator<T> SpliterateAny<T>(this T[] arr, SearchValues<T> separator)
        where T : IEquatable<T>? => new(arr.AsSpan(), separator, SplitType.Any);
    public static SearchSpliterator<T> SpliterateAnyExcept<T>(this T[] arr, SearchValues<T> separator)
        where T : IEquatable<T>? => new(arr.AsSpan(), separator, SplitType.AnyExcept);
    #endregion

    #region Strings
    public static SpanSpliterator<char> Spliterate(this string str, ReadOnlySpan<char> separator) => new(str.AsSpan(), separator);
    public static SpanSpliterator<char> SpliterateAny(this string str, ReadOnlySpan<char> separator) => new(str.AsSpan(), separator, SplitType.Any);
    public static SpanSpliterator<char> SpliterateAnyExcept(this string str, ReadOnlySpan<char> separator) => new(str.AsSpan(), separator, SplitType.AnyExcept);

    public static SingleSpliterator<char> Spliterate(this string str, char separator) => new(str.AsSpan(), separator);
    public static SingleSpliterator<char> SpliterateAnyExcept(this string str, char separator) => new(str.AsSpan(), separator, SplitType.AnyExcept);

    public static SearchSpliterator<char> SpliterateAny(this string str, SearchValues<char> separator) => new(str.AsSpan(), separator, SplitType.Any);
    public static SearchSpliterator<char> SpliterateAnyExcept(this string str, SearchValues<char> separator) => new(str.AsSpan(), separator, SplitType.AnyExcept);
    #endregion
}
