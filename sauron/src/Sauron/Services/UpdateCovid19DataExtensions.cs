using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sauron.Models.Elasticsearch;

namespace Sauron.Services
{
    public static class UpdateCovid19DataExtensions
    {
        public static string ConvertToBulkInsertPayload<TDocument>(this TDocument document, string id)
        {
            return $"{{\"index\": {{\"_id\": \"{id}\"}}}}{Environment.NewLine}{JsonConvert.SerializeObject(document)}";
        }

        public static string CreateBulkInsertPayload<TDocument>(this IEnumerable<TDocument> documents)
            where TDocument : ElasticsearchDocument
        {
            var payload = string.Join($"{Environment.NewLine}",
                documents.Select(doc => doc.ConvertToBulkInsertPayload(doc.Id)).ToList());

            return string.Concat(payload, $"{Environment.NewLine}");
        }
    }
}