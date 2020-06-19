using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Sauron.Services
{
    public static class UpdateCovid19DataExtensions
    {
        public static string ConvertToBulkInsertPayload<TDocument>(this TDocument document)
        {
            return $"{{\"index\": {{}}}}{Environment.NewLine}{JsonConvert.SerializeObject(document)}";
        }

        public static string CreateBulkInsertPayload<TDocument>(this IEnumerable<TDocument> documents)
        {
            var payload = string.Join($"{Environment.NewLine}",
                documents.Select(doc => doc.ConvertToBulkInsertPayload()).ToList());

            return string.Concat(payload, $"{Environment.NewLine}");
        }
    }
}