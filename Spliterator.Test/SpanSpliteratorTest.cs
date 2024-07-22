namespace Spliterator.Test;

// Tests for SpanSpliterator are the overloads that take ReadOnlySpan<T> separators
public class SpanSpliteratorTest
{
    [Fact]
    public void Spliterate_Between_String()
    {
        string data = "ducats:=1p | 45:ducats:=3p | 65:ducats:=4p | 100:ducats:=7p";
        List<string> values = [];

        foreach (var span in data.Spliterate(" | "))
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
    public void SpliterateAny_Between_String()
    {
        string data = "ducats:=1p|45:ducats:=3p!65:ducats:=4p?100:ducats:=7p";
        List<string> values = [];

        foreach (var span in data.SpliterateAny("?!|"))
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
        string data = "101010100011<1000100!1011>100000";
        List<string> values = [];

        foreach (var span in data.SpliterateAnyExcept("01"))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(4, values.Count);
        Assert.Equal("101010100011", values[0]);
        Assert.Equal("1000100", values[1]);
        Assert.Equal("1011", values[2]);
        Assert.Equal("100000", values[3]);
    }

    [Fact]
    public void Spliterate_Leading_String()
    {
        string data = "!!pause!!act!!end";
        List<string> values = [];

        foreach (var span in data.Spliterate("!!"))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("pause", values[0]);
        Assert.Equal("act", values[1]);
        Assert.Equal("end", values[2]);
    }

    [Fact]
    public void SpliterateAny_Leading_String()
    {
        string data = "!pause?act|end";
        List<string> values = [];

        foreach (var span in data.SpliterateAny("?!|"))
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
        string data = "!101010100011 1000100|100000";
        List<string> values = [];

        foreach (var span in data.SpliterateAnyExcept("01"))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("101010100011", values[0]);
        Assert.Equal("1000100", values[1]);
        Assert.Equal("100000", values[2]);
    }

    [Fact]
    public void Spliterate_Trailing_String()
    {
        string data = "a+b>>!c+d>>!e>>!";
        List<string> values = [];

        foreach (var span in data.Spliterate(">>!"))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("a+b", values[0]);
        Assert.Equal("c+d", values[1]);
        Assert.Equal("e", values[2]);
    }

    [Fact]
    public void SpliterateAny_Trailing_String()
    {
        string data = "a+b<c+d!e>";
        List<string> values = [];

        foreach (var span in data.SpliterateAny("<!>"))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("a+b", values[0]);
        Assert.Equal("c+d", values[1]);
        Assert.Equal("e", values[2]);
    }

    [Fact]
    public void SpliterateAnyExcept_Trailing_String()
    {
        string data = "1001001001A101111>";
        List<string> values = [];

        foreach (var span in data.SpliterateAnyExcept("01"))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(2, values.Count);
        Assert.Equal("1001001001", values[0]);
        Assert.Equal("101111", values[1]);
    }

    [Fact]
    public void Spliterate_EmptyValue_Between_String()
    {
        string data = "shop |  | amazon";
        List<string> values = [];

        foreach (var span in data.Spliterate(" | "))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("shop", values[0]);
        Assert.Empty(values[1]);
        Assert.Equal("amazon", values[2]);
    }

    [Fact]
    public void SpliterateAny_EmptyValue_Between_String()
    {
        string data = "shop><amazon";
        List<string> values = [];

        foreach (var span in data.SpliterateAny("<>"))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("shop", values[0]);
        Assert.Empty(values[1]);
        Assert.Equal("amazon", values[2]);
    }

    [Fact]
    public void SpliterateAnyExcept_EmptyValue_Between_String()
    {
        string data = ".$-$$-$.";
        List<string> values = [];

        foreach (var span in data.SpliterateAnyExcept(".-"))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(5, values.Count);
        Assert.Equal(".", values[0]);
        Assert.Equal("-", values[1]);
        Assert.Empty(values[2]);
        Assert.Equal("-", values[3]);
        Assert.Equal(".", values[4]);
    }

    // Rest are done with sequence only

    [Fact]
    public void Spliterate_Between_Numeric()
    {
        ReadOnlySpan<int> data =
        [
            2, 25, 330, 23, 66,
            0, 0,
            232, 10, 1409,
            0, 0,
            239, 2900
        ];
        List<int[]> values = [];

        foreach (var span in data.Spliterate([0, 0]))
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
            0, 1,
            232, 10, 1409,
            0, 1,
            2, 25, 330, 23, 66,
            0, 1,
            239, 2900
        ];
        List<int[]> values = [];

        foreach (var span in data.Spliterate([0, 1]))
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
            1, 1,
            131,
            1, 1,
            22, 16,
            1, 1,
            13, 6503,
            1, 1
        ];
        List<int[]> values = [];

        foreach (var span in data.Spliterate([1, 1]))
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
            1, 2,
            1, 2,
            44, 58,
            1, 2,
            71, 79,
        ];
        List<int[]> values = [];

        foreach (var span in data.Spliterate([1, 2]))
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
            object0, object0,
            object10, object10, object10,
            object0, object0,
            object10, object5, object5
        ];
        List<EquatableObject[]> values = [];

        foreach (var span in data.Spliterate([object0, object0]))
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
            object0, object0,
            object5, object5, object10,
            object0, object0,
            object10, object10, object10,
            object0, object0,
            object10, object5, object5
        ];
        List<EquatableObject[]> values = [];

        foreach (var span in data.Spliterate([object0, object0]))
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
            object0, object0,
            object10, object10, object10,
            object0, object0,
            object10, object5, object5,
            object0, object0
        ];
        List<EquatableObject[]> values = [];

        foreach (var span in data.Spliterate([object0, object0]))
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
            object0, object0,
            object10, object10, object10,
            object0, object0,
            object0, object0,
            object10, object5, object5
        ];
        List<EquatableObject[]> values = [];

        foreach (var span in data.Spliterate([object0, object0]))
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