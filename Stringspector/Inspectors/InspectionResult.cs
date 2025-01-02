namespace Stringspector.Inspectors;

public record InspectionResult(string Header, string Body)
{
    public InspectionResult(string header, int number)
        : this(header, number.ToString("N0"))
    {
    }

    public InspectionResult(string header, double number)
        : this(header, number.ToString("#,##0.###"))
    {
    }
};