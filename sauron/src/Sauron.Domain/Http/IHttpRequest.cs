using System;
using System.Threading.Tasks;

namespace Sauron.Domain.Http
{
    public interface IHttpRequest
    {
        Task<string> Download(string url);
    }
}
