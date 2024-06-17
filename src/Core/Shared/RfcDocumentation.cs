using System.Net;

namespace TaskManagement.Shared
{
    public static class RfcDocumentation
    {
        private readonly static Dictionary<short, string> httpStatusCodeDocumentation = new()
        {
            { 100, "https://www.rfc-editor.org/rfc/rfc9110#section-15.2.1"},
            { 101, "https://www.rfc-editor.org/rfc/rfc9110#section-15.2.2"},
            { 200, "https://www.rfc-editor.org/rfc/rfc9110#section-15.3.1"},
            { 201, "https://www.rfc-editor.org/rfc/rfc9110#section-15.3.2"},
            { 202, "https://www.rfc-editor.org/rfc/rfc9110#section-15.3.3"},
            { 203, "https://www.rfc-editor.org/rfc/rfc9110#section-15.3.4"},
            { 204, "https://www.rfc-editor.org/rfc/rfc9110#section-15.3.5"},
            { 205, "https://www.rfc-editor.org/rfc/rfc9110#section-15.3.6"},
            { 206, "https://www.rfc-editor.org/rfc/rfc9110#section-15.3.7"},
            { 300, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.1"},
            { 301, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.2"},
            { 302, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.3"},
            { 303, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.4"},
            { 304, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.5"},
            { 305, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.6"},
            { 306, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.7"},
            { 307, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.8"},
            { 308, "https://www.rfc-editor.org/rfc/rfc9110#section-15.4.9"},
            { 400, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.1"},
            { 401, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.2"},
            { 402, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.3"},
            { 403, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.4"},
            { 404, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.5"},
            { 405, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.6"},
            { 406, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.7"},
            { 407, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.8"},
            { 408, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.9"},
            { 409, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.10"},
            { 410, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.11"},
            { 411, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.12"},
            { 412, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.13"},
            { 413, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.14"},
            { 414, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.15"},
            { 415, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.16"},
            { 416, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.17"},
            { 417, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.18"},
            { 418, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.19"},
            { 421, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.20"},
            { 422, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.21"},
            { 426, "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.22"},
            { 500, "https://www.rfc-editor.org/rfc/rfc9110#section-15.6.1"},
            { 501, "https://www.rfc-editor.org/rfc/rfc9110#section-15.6.2"},
            { 502, "https://www.rfc-editor.org/rfc/rfc9110#section-15.6.3"},
            { 503, "https://www.rfc-editor.org/rfc/rfc9110#section-15.6.4"},
            { 504, "https://www.rfc-editor.org/rfc/rfc9110#section-15.6.5"},
            { 505, "https://www.rfc-editor.org/rfc/rfc9110#section-15.6.6"}
        };

        public static string? GetDocumentation(HttpStatusCode httpStatusCode)
        {
            var httpStatus = (short)httpStatusCode;
            return httpStatusCodeDocumentation!.ContainsKey(httpStatus) ? httpStatusCodeDocumentation[(short)httpStatusCode] : default;
        }
    }
}
