using BlazingDev.BlazingExtensions;

namespace Stringspector.Inspectors;

public class MagicNumbersInspector
{
    private static List<(string Number, string Header, string Description)> _magicNumbers = new();

    static MagicNumbersInspector()
    {
        var header = "Magic numbers";

        void Add(object number, string description)
        {
            _magicNumbers.Add((number.ToString()!, header, description));
        }

        Add(60 * 60, "Seconds of 1 hour");
        Add(60 * 60 * 24, "Seconds of 1 day");
        Add(60 * 24, "Minutes of 1 day");

        Add(byte.MaxValue, "int8 max value");
        Add(short.MinValue, "int16 min value");
        Add(short.MaxValue, "int16 max value");
        Add(ushort.MaxValue, "uint16 max value");
        Add(int.MinValue, "int32 min value");
        Add(int.MaxValue, "int32 max value");
        Add(uint.MaxValue, "uint32 max value");
        Add(long.MinValue, "int64 min value");
        Add(long.MaxValue, "int64 max value");
        Add(ulong.MaxValue, "uint64 max value");

        header = "HTTP Status Code";
        Add(100, "Continue");
        Add(101, "Switching Protocols");
        Add(102, "Processing");
        Add(103, "Early Hints");
        Add(200, "OK");
        Add(201, "Created");
        Add(202, "Accepted");
        Add(203, "Non-Authoritative Information");
        Add(204, "No Content");
        Add(205, "Reset Content");
        Add(206, "Partial Content");
        Add(207, "Multi-Status");
        Add(208, "Already Reported");
        Add(226, "IM Used");
        Add(300, "Multiple Choices");
        Add(301, "Moved Permanently");
        Add(302, "Found");
        Add(303, "See Other");
        Add(304, "Not Modified");
        Add(305, "Use Proxy");
        Add(307, "Temporary Redirect");
        Add(308, "Permanent Redirect");
        Add(400, "Bad Request");
        Add(401, "Unauthorized");
        Add(402, "Payment Required");
        Add(403, "Forbidden");
        Add(404, "Not Found");
        Add(405, "Method Not Allowed");
        Add(406, "Not Acceptable");
        Add(407, "Proxy Authentication Required");
        Add(408, "Request Timeout");
        Add(409, "Conflict");
        Add(410, "Gone");
        Add(411, "Length Required");
        Add(412, "Precondition Failed");
        Add(413, "Payload Too Large");
        Add(414, "URI Too Long");
        Add(415, "Unsupported Media Type");
        Add(416, "Range Not Satisfiable");
        Add(417, "Expectation Failed");
        Add(418, "I'm a teapot");
        Add(421, "Misdirected Request");
        Add(422, "Unprocessable Entity");
        Add(423, "Locked");
        Add(424, "Failed Dependency");
        Add(425, "Too Early");
        Add(426, "Upgrade Required");
        Add(428, "Precondition Required");
        Add(429, "Too Many Requests");
        Add(431, "Request Header Fields Too Large");
        Add(451, "Unavailable For Legal Reasons");
        Add(500, "Internal Server Error");
        Add(501, "Not Implemented");
        Add(502, "Bad Gateway");
        Add(503, "Service Unavailable");
        Add(504, "Gateway Timeout");
        Add(505, "HTTP Version Not Supported");
        Add(506, "Variant Also Negotiates");
        Add(507, "Insufficient Storage");
        Add(508, "Loop Detected");
        Add(510, "Not Extended");
        Add(511, "Network Authentication Required");
    }

    public IEnumerable<InspectionResult> Inspect(string text)
    {
        text = text.Trim();
        foreach (var item in _magicNumbers)
        {
            if (item.Number == text)
            {
                yield return new InspectionResult(item.Header, item.Description);
            }
        }
    }
}