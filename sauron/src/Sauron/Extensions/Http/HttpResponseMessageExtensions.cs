using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sauron.Extensions.Http
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task CheckIsSuccessStatusCode(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new OperationCanceledException(message);
            }
        }
    }
}