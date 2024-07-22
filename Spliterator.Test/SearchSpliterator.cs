using System.Buffers;

namespace Spliterator.Test;

// Tests for SearchSpliterator are the overloads that take a SearchValues<T> separator
public class SearchSpliterator
{
    [Fact]
    public void SpliterateAny_Between_String()
    {
        string data = "ducats:=1p|45:ducats:=3p!65:ducats:=4p?100:ducats:=7p";
        List<string> values = [];

        var search = SearchValues.Create("?!|");
        foreach (var span in data.SpliterateAny(search))
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

        var search = SearchValues.Create("01");
        foreach (var span in data.SpliterateAnyExcept(search))
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
    public void SpliterateAny_Leading_String()
    {
        string data = "!pause?act|end";
        List<string> values = [];

        var search = SearchValues.Create("?!|");
        foreach (var span in data.SpliterateAny(search))
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

        var search = SearchValues.Create("01");
        foreach (var span in data.SpliterateAnyExcept(search))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(3, values.Count);
        Assert.Equal("101010100011", values[0]);
        Assert.Equal("1000100", values[1]);
        Assert.Equal("100000", values[2]);
    }

    [Fact]
    public void SpliterateAny_Trailing_String()
    {
        string data = "a+b<c+d!e>";
        List<string> values = [];

        var search = SearchValues.Create("<!>");
        foreach (var span in data.SpliterateAny(search))
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

        var search = SearchValues.Create("01");
        foreach (var span in data.SpliterateAnyExcept(search))
        {
            values.Add(span.ToString());
        }

        Assert.Equal(2, values.Count);
        Assert.Equal("1001001001", values[0]);
        Assert.Equal("101111", values[1]);
    }

    [Fact]
    public void SpliterateAny_EmptyValue_Between_String()
    {
        string data = "shop><amazon";
        List<string> values = [];

        var search = SearchValues.Create("<>");
        foreach (var span in data.SpliterateAny(search))
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

        var search = SearchValues.Create(".-");
        foreach (var span in data.SpliterateAnyExcept(search))
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
}
