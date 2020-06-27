namespace Covid19.Monitor.Sv.Models
{
    public class Supply
    {
        public SupplyType Type { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
        public string Link { get; set; }
    }
}