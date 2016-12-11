namespace FundManager.Model
{
    public class InstrumentSummary
    {
        public InstrumentTypeEnum InstrumentType { get; set; }
        public decimal TotalNumber { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal TotalMarketValue { get; set; }
    }
}
