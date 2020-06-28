using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Covid19.Monitor.Ui.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Covid19.Monitor.Ui.Controllers
{
    [Route("panel")]
    public class PanelController : Controller
    {
        private readonly ConnectionInfo _connection;
        private readonly Uri _backendUri;
        private readonly string[] _regionCodes;

        public PanelController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _connection = httpContextAccessor.HttpContext.Connection;
            _backendUri = new Uri(configuration.GetValue<string>("BackendUrl"));
            _regionCodes = new[]
            {
                "AC","AL","AP","AM","BA","CE","DF","ES","GO","MA",
                "MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN",
                "RS","RO","RR","SC","SP","SE","TO"
            };
        }

        public async Task<IActionResult> Index([FromServices] IConfiguration configuration)
        {
            var model = new PanelViewModel
            {
                ClientRemoteIpv4 = _connection.RemoteIpAddress.MapToIPv4().ToString(),
                CurrentCases = new int[12],
                NewCases = new int[12],
                CurrentDeaths = new int[12],
                NewDeaths = new int[12],
                Cases = new List<TableViewModel>(),
                Deaths = new List<TableViewModel>()
            };

            try
            {
                using var httpClient = new HttpClient
                {
                    BaseAddress = _backendUri
                };

                var jsonAllCases = await httpClient.GetStringAsync("api/cases/month");
                var resultAllCases = JsonSerializer.Deserialize<List<BackendGroupedCasesResponse>>(jsonAllCases);

                foreach (var item in resultAllCases)
                {
                    model.CurrentCases[item.Month - 1] = item.CurrentCases ?? 0;
                    model.NewCases[item.Month - 1] = item.NewCases ?? 0;
                    model.CurrentDeaths[item.Month - 1] = item.CurrentDeaths ?? 0;
                    model.NewDeaths[item.Month - 1] = item.NewDeaths ?? 0;
                }

                foreach (var regionCode in _regionCodes)
                {
                    var json = await httpClient.GetStringAsync($"api/cases/{regionCode}/month");
                    var result = JsonSerializer.Deserialize<List<BackendGroupedCasesResponse>>(json);
                    var info = result.OrderByDescending(r => r.Month).FirstOrDefault();
                    model.Cases.Add(new TableViewModel
                    {
                        Estado = regionCode,
                        Atual = info?.CurrentCases ?? 0,
                        Novos = info?.NewCases ?? 0
                    });
                    
                    model.Deaths.Add(new TableViewModel
                    {
                        Estado = regionCode,
                        Atual = info?.CurrentDeaths ?? 0,
                        Novos = info?.NewDeaths ?? 0
                    });
                }
            }
            catch (Exception e)
            {
                model.Message = e.Message;
            }

            return View(model);
        }
    }
}