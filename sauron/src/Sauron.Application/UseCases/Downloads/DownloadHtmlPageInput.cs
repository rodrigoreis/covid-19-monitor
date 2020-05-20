using System;

namespace Sauron.Application.UseCases.Downloads
{
    public class DownloadHtmlPageInput
    {
        public Uri Uri { get; }

        private DownloadHtmlPageInput() { }

        public DownloadHtmlPageInput(string uri) : this()
        {
            Uri = new Uri(uri);
        }
    }
}
