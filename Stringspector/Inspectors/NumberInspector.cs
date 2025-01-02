namespace Stringspector.Inspectors;

public class NumberInspector
{
    public IEnumerable<InspectionResult> Inspect(string text)
    {
        var isInteger = int.TryParse(text, out var asInteger);
        if (isInteger)
        {
            yield return new InspectionResult("int as hex", "0x" + Convert.ToString(asInteger, 16).ToUpper());
            var asCharacter = (char)asInteger;
            if (char.IsAscii(asCharacter))
            {
                if (!char.IsControl(asCharacter))
                {
                    yield return new InspectionResult("int as char", asCharacter.ToString().Replace(" ", "SPACE"));
                }
                else
                {
                    var explanation = asInteger switch
                    {
                        0 => "NUL",
                        1 => "SOH",
                        2 => "STX",
                        3 => "ETX",
                        4 => "EOT",
                        5 => "ENQ",
                        6 => "ACK",
                        7 => "BEL",
                        8 => "BS",
                        9 => "HT",
                        10 => "LF",
                        11 => "VT",
                        12 => "FF",
                        13 => "CR",
                        14 => "SO",
                        15 => "SI",
                        16 => "DLE",
                        17 => "DC1",
                        18 => "DC2",
                        19 => "DC3",
                        20 => "DC4",
                        21 => "NAK",
                        22 => "SYN",
                        23 => "ETB",
                        24 => "CAN",
                        25 => "EM",
                        26 => "SUM",
                        27 => "ESC",
                        28 => "FS",
                        29 => "GS",
                        30 => "RS",
                        31 => "US",
                        32 => "SPACE",
                        _ => throw new InvalidOperationException("that should not happen")
                    };
                    yield return new InspectionResult("int as char", $"Control character: " + explanation);
                }
            }
        }

        var isLong = long.TryParse(text, out var asLong);
        if (isLong)
        {
            var oneYearTicks = TimeSpan.FromDays(365).TotalMilliseconds;
            if (asLong > oneYearTicks)
            {
                var secondsSinceUnix = DateTime.UnixEpoch.AddMilliseconds(asLong);
                yield return new InspectionResult("number as seconds since unix epoch / JS ticks",
                    secondsSinceUnix.ToString("o"));
            }
        }
    }
}