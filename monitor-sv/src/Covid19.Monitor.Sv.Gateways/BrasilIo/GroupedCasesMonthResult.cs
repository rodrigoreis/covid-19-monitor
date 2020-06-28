namespace Covid19.Monitor.Sv.Gateways.BrasilIo
{
    public class GroupedCasesMonthResult
    {
        public int Month { get; set; }
        public int? CurrentCases { get; set; }
        public int? NewCases { get; set; }
        public int? CurrentDeaths { get; set; }
        public int? NewDeaths { get; set; }
    }
}