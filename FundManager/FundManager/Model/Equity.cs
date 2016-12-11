namespace FundManager.Model
{
    //This class has been made public only to perform unit tests.
    //InstrumentFactoryTests.InstrumentTypeEquityRequestReturnsEquityType

    public class Equity: Instrument
    {
        // tolerance can me made a configurable item in app.config, if necessary
        private const int tolerance = 200000;

        public override int Tolerance => tolerance;
        public override decimal TransactionCost => MarketValue*(decimal) (0.5/100);
    }
}
