namespace Spliterator.Test;

// Tests for SingleSpliterator are the overloads that take a T separator
public class SingleSpliteratorTest
{
    [Fact]
    public void Spliterate_Between_String()
    {
        string data = "ducats:=1p|45:ducats:=3p|65:ducats:=4p|100:ducats:=7p";
        List<string> values = [];

        foreach (var span in data.Spliterate('|'))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(4, values.Count);
        Assert.Equal("ducats:=1p", values[0]);
        Assert.Equal("45:ducats:=3p", values[1]);
        Assert.Equal("65:ducats:=4p", values[2]);
        Assert.Equal("100:ducats:=7p", values[3]);
    }

    [Fact]
    public void SpliterateAnyExcept_Between_String()
    {
        string data = "00000-00-0000";
        List<string> values = [];

        foreach (var span in data.SpliterateAnyExcept('0'))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("00000", values[0]);
        Assert.Equal("00", values[1]);
        Assert.Equal("0000", values[2]);
    }

    [Fact]
    public void Spliterate_Leading_String()
    {
        string data = "!pause!act!end";
        List<string> values = [];

        foreach (var span in data.Spliterate('!'))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("pause", values[0]);
        Assert.Equal("act", values[1]);
        Assert.Equal("end", values[2]);
    }

    [Fact]
    public void SpliterateAnyExcept_Leading_String()
    {
        string data = "!00000 00|000000";
        List<string> values = [];

        foreach (var span in data.SpliterateAnyExcept('0'))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("00000", values[0]);
        Assert.Equal("00", values[1]);
        Assert.Equal("000000", values[2]);
    }

    [Fact]
    public void Spliterate_Trailing_String()
    {
        string data = "_de127_F_14ac_F_b_F";
        List<string> values = [];

        foreach (var span in data.Spliterate('F'))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("_de127_", values[0]);
        Assert.Equal("_14ac_", values[1]);
        Assert.Equal("_b_", values[2]);
    }

    [Fact]
    public void SpliterateAnyExcept_Trailing_String()
    {
        string data = "111111121111114";
        List<string> values = [];

        foreach (var span in data.SpliterateAnyExcept('1'))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(2, values.Count);
        Assert.Equal("1111111", values[0]);
        Assert.Equal("111111", values[1]);
    }

    [Fact]
    public void Spliterate_EmptyValue_Between_String()
    {
        string data = "why|shop||amazon";
        List<string> values = [];

        foreach (var span in data.Spliterate('|'))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(4, values.Count);
        Assert.Equal("why", values[0]);
        Assert.Equal("shop", values[1]);
        Assert.Empty(values[2]);
        Assert.Equal("amazon", values[3]);
    }

    [Fact]
    public void SpliterateAnyExcept_EmptyValue_Between_String()
    {
        string data = ".$.€$.€.";
        List<string> values = [];

        foreach (var span in data.SpliterateAnyExcept('.'))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(5, values.Count);
        Assert.Equal(".", values[0]);
        Assert.Equal(".", values[1]);
        Assert.Empty(values[2]);
        Assert.Equal(".", values[3]);
        Assert.Equal(".", values[4]);
    }


    [Fact]
    public void Spliterate_Between_Numeric()
    {
        ReadOnlySpan<int> data =
        [
            2, 25, 330, 23, 66,
            0,
            232, 10, 1409,
            0,
            239, 2900
        ];
        List<int[]> values = [];

        foreach (var span in data.Spliterate(0))
        {
            values.Add(span.ToArray());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal([2, 25, 330, 23, 66], values[0]);
        Assert.Equal([232, 10, 1409], values[1]);
        Assert.Equal([239, 2900], values[2]);
    }

    [Fact]
    public void Spliterate_Leading_Numeric()
    {
        ReadOnlySpan<int> data =
        [
            0,
            232, 10, 1409,
            0,
            2, 25, 330, 23, 66,
            0,
            239, 2900
        ];
        List<int[]> values = [];

        foreach (var span in data.Spliterate(0))
        {
            values.Add(span.ToArray());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal([232, 10, 1409], values[0]);
        Assert.Equal([2, 25, 330, 23, 66], values[1]);
        Assert.Equal([239, 2900], values[2]);
    }

    [Fact]
    public void Spliterate_Trailing_Numeric()
    {
        ReadOnlySpan<int> data =
        [
            3493, 223, 1980,
            1,
            131,
            1,
            22, 16,
            1,
            13, 6503,
            1,
        ];
        List<int[]> values = [];

        foreach (var span in data.Spliterate(1))
        {
            values.Add(span.ToArray());
        }

        Assert.Equal(4, values.Count);
        Assert.Equal([3493, 223, 1980], values[0]);
        Assert.Equal([131], values[1]);
        Assert.Equal([22, 16], values[2]);
        Assert.Equal([13, 6503], values[3]);
    }

    [Fact]
    public void Spliterate_EmptyValue_Between_Numeric()
    {
        ReadOnlySpan<int> data =
        [
            10, 20, 32,
            1,
            1,
            44, 58,
            1,
            71, 79,
        ];
        List<int[]> values = [];

        foreach (var span in data.Spliterate(1))
        {
            values.Add(span.ToArray());
        }

        Assert.Equal(4, values.Count);
        Assert.Equal([10, 20, 32], values[0]);
        Assert.Empty(values[1]);
        Assert.Equal([44, 58], values[2]);
        Assert.Equal([71, 79], values[3]);
    }

    [Fact]
    public void Spliterate_Between_Object()
    {
        EquatableObject object0 = new(0);
        EquatableObject object5 = new(5);
        EquatableObject object10 = new(10);
        ReadOnlySpan<EquatableObject> data =
        [
            object5, object5, object10,
            object0,
            object10, object10, object10,
            object0,
            object10, object5, object5
        ];
        List<EquatableObject[]> values = [];

        foreach (var span in data.Spliterate(object0))
        {
            values.Add(span.ToArray());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal([object5, object5, object10], values[0]);
        Assert.Equal([object10, object10, object10], values[1]);
        Assert.Equal([object10, object5, object5], values[2]);
    }

    [Fact]
    public void Spliterate_Leading_Object()
    {
        EquatableObject object0 = new(0);
        EquatableObject object5 = new(5);
        EquatableObject object10 = new(10);
        ReadOnlySpan<EquatableObject> data =
        [
            object0,
            object5, object5, object10,
            object0,
            object10, object10, object10,
            object0,
            object10, object5, object5
        ];
        List<EquatableObject[]> values = [];

        foreach (var span in data.Spliterate(object0))
        {
            values.Add(span.ToArray());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal([object5, object5, object10], values[0]);
        Assert.Equal([object10, object10, object10], values[1]);
        Assert.Equal([object10, object5, object5], values[2]);
    }

    [Fact]
    public void Spliterate_Trailing_Object()
    {
        EquatableObject object0 = new(0);
        EquatableObject object5 = new(5);
        EquatableObject object10 = new(10);
        ReadOnlySpan<EquatableObject> data =
        [
            object5, object5, object10,
            object0,
            object10, object10, object10,
            object0,
            object10, object5, object5,
            object0,
        ];
        List<EquatableObject[]> values = [];

        foreach (var span in data.Spliterate(object0))
        {
            values.Add(span.ToArray());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal([object5, object5, object10], values[0]);
        Assert.Equal([object10, object10, object10], values[1]);
        Assert.Equal([object10, object5, object5], values[2]);
    }

    [Fact]
    public void Spliterate_EmptyValue_Between_Object()
    {
        EquatableObject object0 = new(0);
        EquatableObject object5 = new(5);
        EquatableObject object10 = new(10);
        ReadOnlySpan<EquatableObject> data =
        [
            object5, object5, object10,
            object0,
            object10, object10, object10,
            object0,
            object0,
            object10, object5, object5
        ];
        List<EquatableObject[]> values = [];

        foreach (var span in data.Spliterate(object0))
        {
            values.Add(span.ToArray());
        }

        Assert.Equal(4, values.Count);
        Assert.Equal([object5, object5, object10], values[0]);
        Assert.Equal([object10, object10, object10], values[1]);
        Assert.Empty(values[2]);
        Assert.Equal([object10, object5, object5], values[3]);
    }
}
