using System.Buffers;
using System.Runtime.CompilerServices;

namespace Spliterator;

public ref struct SearchSpliterator<T>(ReadOnlySpan<T> span, SearchValues<T> separators, SplitType splitType = default)
    where T : IEquatable<T>?
{
    readonly SplittingEnumerator _enumerator = new(span, separators, splitType);

    public readonly SplittingEnumerator GetEnumerator() => _enumerator;

    public ref struct SplittingEnumerator(ReadOnlySpan<T> span, SearchValues<T> separators, SplitType splitType = default)
    {
        readonly ReadOnlySpan<T> _span = span;
        readonly SearchValues<T> _separators = separators;
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
                SplitType.Any => _span[_start..].IndexOfAny(_separators) + _start,
                SplitType.AnyExcept => _span[_start..].IndexOfAnyExcept(_separators) + _start,
                _ => throw new NotSupportedException("SearchSpliterator cannot use IndexOf"),
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
