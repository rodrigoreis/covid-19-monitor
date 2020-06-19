using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sauron.Services
{
    public interface IElasticsearchService
    {
        Task BulkInsert<T>(string index, IEnumerable<T> documents);
    }
}