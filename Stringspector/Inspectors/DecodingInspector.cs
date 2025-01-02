using System.Text;
using System.Web;

namespace Stringspector.Inspectors;

public class DecodingInspector
{
    public IEnumerable<InspectionResult> Inspect(string text)
    {
        text = text.Trim();
        var result = new List<InspectionResult>();
        CheckForBase64(result, text);
        CheckForUrl(result, text);
        CheckForHttp(result, text);
        return result;
    }

    private void CheckForBase64(List<InspectionResult> result, string text)
    {
        if (text.Length % 4 == 0)
        {
            try
            {
                var bytes = Convert.FromBase64String(text);
                result.Add(new InspectionResult("Base64", true.ToString()));

                var asUtf8 = Encoding.UTF8.GetString(bytes);
                var isPrintable = asUtf8.All(c => !char.IsControl(c) || char.IsWhiteSpace(c));
                if (isPrintable)
                {
                    result.Add(new InspectionResult("Base64 to UTF8", asUtf8));
                }
            }
            catch (FormatException)
            {
            }
        }
    }

    private void CheckForUrl(List<InspectionResult> result, string text)
    {
        var decoded = Uri.UnescapeDataString(text);
        if (decoded != text)
        {
            result.Add(new InspectionResult("URL decoded", decoded));
        }
    }

    private void CheckForHttp(List<InspectionResult> result, string text)
    {
        var decoded = HttpUtility.HtmlDecode(text);
        if (decoded != text)
        {
            result.Add(new InspectionResult("HTML decoded", decoded));
        }
    }
}