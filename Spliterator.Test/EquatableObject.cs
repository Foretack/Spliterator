namespace Spliterator.Test;

public class EquatableObject(int value): IEquatable<EquatableObject>
{
    public int Value { get; } = value;

    public bool Equals(EquatableObject? other) => other is not null && this.Value == other.Value;
}
