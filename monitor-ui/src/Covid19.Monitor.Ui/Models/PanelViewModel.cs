using System.Collections.Generic;
using System.Text.Json;

namespace Covid19.Monitor.Ui.Models
{
    public class PanelViewModel
    {
        public string Message { get; set; }
        public string ClientRemoteIpv4 { get; set; }
        public int[] NewCases { get; set; }
        public int[] CurrentCases { get; set; }
        public int[] NewDeaths { get; set; }
        public int[] CurrentDeaths { get; set; }
        public List<TableViewModel> Cases { get; set; }
        public List<TableViewModel>  Deaths { get; set; }
    }
}