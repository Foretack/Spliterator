using System.Runtime.CompilerServices;

namespace Spliterator;

public ref struct SingleSpliterator<T>(ReadOnlySpan<T> span, T separator, SplitType splitType = default)
    where T : IEquatable<T>?
{
    readonly SplittingEnumerator _enumerator = new(span, separator, splitType);

    public readonly SplittingEnumerator GetEnumerator() => _enumerator;

    public ref struct SplittingEnumerator(ReadOnlySpan<T> span, T separator, SplitType splitType = default)
    {
        readonly ReadOnlySpan<T> _span = span;
        readonly T _separator = separator;
        readonly SplitType _sType = splitType;
        int _start;
        int _end;

        public bool MoveNext()
        {
            if (_end == _span.Length)
            {
                return false;
            }

            if (_end != 0)
            {
                _start = _end + 1;
            }

            if (_start >= _span.Length)
            {
                return false;
            }

            _end = _sType switch
            {
                SplitType.AnyExcept => _span[_start..].IndexOfAnyExcept(_separator) + _start,
                _ => _span[_start..].IndexOf(_separator) + _start,
            };

            if (_end == 0)
            {
                _start = _end + 1;
                return MoveNext();
            }

            if (_end == _start - 1)
            {
                _end = _span.Length;
            }

            return true;
        }

        public readonly ReadOnlySpan<T> Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _span[_start.._end];
        }
    }
}
