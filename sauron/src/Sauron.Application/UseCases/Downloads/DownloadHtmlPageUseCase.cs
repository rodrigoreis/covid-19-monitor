using System;
using System.Threading.Tasks;

namespace Sauron.Application.UseCases.Downloads
{
    internal class DownloadHtmlPageUseCase : IDownloadHtmlPageUseCase
    {
        public Task Execute(DownloadHtmlPageInput input)
        {
            return Task.Run(() => Console.WriteLine("Executed"));
        }
    }
}
