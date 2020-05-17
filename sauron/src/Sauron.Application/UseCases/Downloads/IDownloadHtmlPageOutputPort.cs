using Sauron.Application.Abstractions.UseCases;

namespace Sauron.Application.UseCases.Downloads
{
    public interface IDownloadHtmlPageOutputPort : IDefaultOutputPort<DownloadHtmlPageOutput>,
                                                   INotFoundOutputPort,
                                                   IInternalServerErrorOutputPort { }
}
