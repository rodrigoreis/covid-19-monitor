using System;
using Sauron.Application.Abstractions.Extensions;

namespace Sauron.Application.UseCases.Downloads
{
    public class DownloadHtmlPageOutput
    {
        private const string SaoPauloTimeZoneId = "America/Sao_Paulo";
        public string HtmlDocument { get; }
        public DateTimeOffset DownloadOn { get; }

        private DownloadHtmlPageOutput() { }

        public DownloadHtmlPageOutput(string htmlDocument) : this()
        {
            HtmlDocument = htmlDocument;
            DownloadOn = DateTime.Now.ToDateTimeOffset(
                TimeZoneInfo.FindSystemTimeZoneById(SaoPauloTimeZoneId).BaseUtcOffset
            );
        }
    }
}
