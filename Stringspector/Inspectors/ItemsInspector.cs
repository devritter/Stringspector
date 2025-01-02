using BlazingDev.BlazingExtensions;

namespace Stringspector.Inspectors;

public class ItemsInspector
{
    public IEnumerable<InspectionResult> Inspect(string text)
    {
        char[] possibleSeparators = [' ', '\t', '\n', ',', ';', '|'];
        var separatorOccurrences = new Dictionary<char, int>();

        foreach (var separator in possibleSeparators)
        {
            var count = text.BzSplit(separator).Length;
            separatorOccurrences.Add(separator, count);
        }

        var max = separatorOccurrences.MaxBy(x => x.Value);
        if (max.Value >= 3)
        {
            var parts = text.BzSplit(max.Key);
            yield return new InspectionResult("Items separator", max.Key.ToString());
            yield return new InspectionResult("Items count", max.Value);

            var distinctCount = parts.Distinct().Count();
            if (distinctCount != max.Value)
            {
                yield return new InspectionResult("Items distinct count", distinctCount);
            }

            var allNumbers = parts
                .Select(x => double.TryParse(x, out var result) ? result : (double?)null)
                .WhereNotNull()
                .ToList();

            if (allNumbers.Count == parts.Length)
            {
                yield return new InspectionResult("Min", allNumbers.Min());
                yield return new InspectionResult("Avg", allNumbers.Average());
                yield return new InspectionResult("Max", allNumbers.Max());
                yield return new InspectionResult("Sum", allNumbers.Sum());
            }
        }
    }
}