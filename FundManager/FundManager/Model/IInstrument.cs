namespace FundManager.Model
{
    public interface IInstrument
    {
        InstrumentTypeEnum InstrumentType { get; }
        int Quantity { get; set; }
        decimal Price { get; set; }
        string Name { get; set; }
        decimal MarketValue { get; }
        decimal TransactionCost { get; }
        decimal Weight { get; set; }
        int Tolerance { get; }
        bool IsInstrumentTolerant { get; }
    }
}
