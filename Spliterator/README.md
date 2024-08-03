# Spliterator

Spliterator adds split extensions to spans, arrays and strings which have zero allocation. I commonly implement something like this in my personal projects, so I thought it's about time I make it a NuGet package.

## Examples

`Spliterate()`:
```csharp
ReadOnlySpan<int> data =
[
    2, 25, 330, 23, 66,
    0, 0,
    232, 10, 1409,
    0, 0,
    239, 2900
];

foreach (var span in data.Spliterate([0, 0]))
{
    Console.WriteLine(span.Length);
}

// The following output is displayed:
//      5
//      3
//      2
```

```csharp
string data = "ducats:=1p|45:ducats:=3p|65:ducats:=4p|100:ducats:=7p";

foreach (var span in data.Spliterate('|'))
{
    Console.WriteLine(span.ToString());
}

// The following output is displayed:
//      ducats:=1p
//      45:ducats:=3p
//      65:ducats:=4p
//      100:ducats:=7p
```

`SpliterateAny()`:
```csharp
string data = "!pause?act|end";

foreach (var span in data.SpliterateAny("?!|"))
{
    Console.WriteLine(span.ToString());
}

// The following output is displayed:
//      pause
//      act
//      end
```

```csharp
string data = "a+b<c+d!e>";

foreach (var span in data.SpliterateAny("<!>"))
{
    Console.WriteLine(span.ToString());
}

// The following output is displayed:
//      a+b
//      c+d
//      e
```

`SpliterateAnyExcept()`:
```csharp
string data = "!101010100011 1000100|100000";

foreach (var span in data.SpliterateAnyExcept("01"))
{
    Console.WriteLine(span.ToString());
}

// The following output is displayed:
//      101010100011
//      1000100
//      100000
```

```csharp
string data = "00000-00-0000";

foreach (var span in data.SpliterateAnyExcept('0'))
{
    Console.WriteLine(span.ToString());
}

// The following output is displayed:
//      00000
//      00
//      0000
```

[NuGet Package Link](https://www.nuget.org/packages/Spliterator)