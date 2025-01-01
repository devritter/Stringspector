using System.Text;
using BlazingDev.BlazingExtensions;

namespace Stringspector.Inspectors;

public class StringInspector
{
    public IEnumerable<InspectionResult> Inspect(string input)
    {
        if (input.Length == 1)
        {
            return InspectSingleCharacter(input[0]);
        }

        return InspectMultiCharacterString(input);
    }

    private IEnumerable<InspectionResult> InspectSingleCharacter(char c)
    {
        if (!char.IsAscii(c))
        {
            yield return new InspectionResult("Is ASCII", char.IsAscii(c).ToString());
            var utf8ByteCount = Encoding.UTF8.GetByteCount(c.ToString());
            if (utf8ByteCount > 1)
            {
                yield return new InspectionResult("UTF8 byte count", utf8ByteCount.ToString());
            }
        }

        yield return new InspectionResult("char as int", ((int)c).ToString());
        yield return new InspectionResult("char as hex", "0x" + Convert.ToString(c, 16).ToUpper());
    }

    private IEnumerable<InspectionResult> InspectMultiCharacterString(string input)
    {
        yield return new InspectionResult("Length", input.Length.ToString("N0"));
        var trimmed = input.Trim();
        foreach (var inspectionResult in InspectTrimming(input, trimmed))
        {
            yield return inspectionResult;
        }

        var lines = input.Split('\n').Length;
        if (lines > 1)
        {
            yield return new InspectionResult("Lines", lines.ToString("N0"));
        }

        var whiteSpaces = input.Count(char.IsWhiteSpace);
        if (whiteSpaces > 0)
        {
            yield return new InspectionResult("White Spaces", whiteSpaces.ToString("N0"));
        }

        var nonAscii = input.Where(x => !char.IsAscii(x)).ToList();
        if (nonAscii.Any())
        {
            yield return new InspectionResult("Non-ASCII character count", nonAscii.Count.ToString("N0"));
            yield return new InspectionResult("Non-ASCII characters", nonAscii.Distinct().StringJoin());
        }
    }

    private IEnumerable<InspectionResult> InspectTrimming(string input, string trimmed)
    {
        if (trimmed != input)
        {
            yield return new InspectionResult("Trimmed Length", trimmed.Length.ToString("N0"));
            var trimmableStart = input.TakeWhile(char.IsWhiteSpace).StringJoin();
            var trimmableEnd = input.AsEnumerable().Reverse().TakeWhile(char.IsWhiteSpace).Reverse().StringJoin();
            trimmableStart = Xray(trimmableStart);
            trimmableEnd = Xray(trimmableEnd);
            if (trimmableStart.HasContent())
            {
                yield return new InspectionResult("Trimmable start", trimmableStart);
            }

            if (trimmableEnd.HasContent())
            {
                yield return new InspectionResult("Trimmable end", trimmableEnd);
            }
        }
    }

    private string Xray(string input)
    {
        return input
            .Replace(" ", "[SPACE]")
            .Replace("\t", "[TAB]")
            .Replace("\r", "[CR]")
            .Replace("\n", "[LF]");
    }
}

public record InspectionResult(string Header, string Body);