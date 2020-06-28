using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Covid19.Monitor.Sv.Gateways.BrasilIo
{
    internal class BrasilIoGateway : IBrasilIoGateway
    {
        private async Task<List<Item>> ListAsync(string state)
        {
            var result = new List<Item>();
            var endpoint =
                $"api/dataset/covid19/caso_full/data/?search=&epidemiological_week=&date=&order_for_place=&state={state.ToUpper()}&city=&city_ibge_code=&place_type=state&last_available_date=&is_last=&is_repeated=";
            
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://brasil.io/")
            };
            
            while (true)
            {
                var json = await httpClient.GetStringAsync(endpoint);
            
                var response = JsonConvert.DeserializeObject<DataApiResponse<Item>>(json);
                
                result.AddRange(response.Results);
                
                if (response.Next != default)
                {
                    endpoint = response.Next;
                    continue;
                }

                break;
            }

            return result;
        }
        
        public async Task<List<CasesMonthResult>> ListByRegionCodeAsync(string regionCode)
        {
            var items = await ListAsync(regionCode);

            var results = items.Select(p => new CasesInfo
            {
                Date = p.Date,
                CurrentConfirmed = p.LastAvailableConfirmed,
                DeathRate = p.LastAvailableDeathRate,
                CurrentConfirmedDeaths = p.LastAvailableDeaths,
                NewConfirmed = p.NewConfirmed,
                NewConfirmedDeaths = p.NewDeaths,
                RegionCode = p.State
            }).ToList();

            var groups = results.GroupBy(r => r.Date.Month);

            if (string.IsNullOrWhiteSpace(regionCode))
            {
                var list = new List<CasesMonthResult>();
                
                foreach (var gp in groups)
                {
                    var gpr = gp
                              .GroupBy(x => x.RegionCode)
                              .Select(x => x.OrderByDescending(y => y.Date).First());

                    var casesInfos = gpr as CasesInfo[] ?? gpr.ToArray();
                    
                    list.Add(new CasesMonthResult
                    {
                        Month = gp.Key,
                        CurrentCases = casesInfos.Sum(x => x.CurrentConfirmed),
                        CurrentDeaths = casesInfos.Sum(x => x.CurrentConfirmedDeaths),
                        NewCases = casesInfos.Sum(x => x.NewConfirmed),
                        NewDeaths = casesInfos.Sum(x => x.NewConfirmedDeaths)
                    });
                }

                return list;
            }
            else
            {
                return groups
                       .Select(gp => gp.OrderByDescending(g => g.Date).First())
                       .Select(od => new CasesMonthResult
                       {
                           Month = od.Date.Month,
                           CurrentCases = od.CurrentConfirmed,
                           CurrentDeaths = od.CurrentConfirmedDeaths,
                           NewCases = od.NewConfirmed,
                           NewDeaths = od.NewConfirmedDeaths
                       })
                       .ToList();                
            }
        }

        public async Task<List<GroupedCasesMonthResult>> ListByRegionCodeGroupByMonthAsync(string regionCode)
        {
            var results = await ListByRegionCodeAsync(regionCode);

            var groupedResult = results.GroupBy(r => r.Month);

            return groupedResult
                   .Select(gp => new GroupedCasesMonthResult
                   {
                       Month = gp.Key,
                       CurrentCases = gp.Sum(i => i.CurrentCases),
                       CurrentDeaths = gp.Sum(i => i.CurrentDeaths),
                       NewCases = gp.Sum(i => i.NewCases),
                       NewDeaths = gp.Sum(i => i.NewDeaths),
                   })
                   .ToList();
        }
    }
}