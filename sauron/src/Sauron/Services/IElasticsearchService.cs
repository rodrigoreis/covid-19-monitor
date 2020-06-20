using System.Collections.Generic;
using System.Threading.Tasks;
using Sauron.Models.Elasticsearch;

namespace Sauron.Services
{
    public interface IElasticsearchService
    {
        Task CreateIndexIfNotExists(string index, string contract);
        
        Task BulkInsert<T>(string index, IEnumerable<T> documents) where T : ElasticsearchDocument;
    }
}