namespace TaskManagement.Infrastructure.OpenApi
{
    internal class SwaggerOptions
    {
        public static readonly string SECTION = "SwaggerSettings";
        public bool Enable { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public Uri? ContactUrl { get; set; }
        public bool License { get; set; }
        public string? LicenseName { get; set; }
        public Uri? LicenseUrl { get; set; }
        public Uri? TermsOfService { get; set; }
        public string? RoutePrefix { get; set; }
    }
}
