namespace FundManager.Model
{
    //This class has been made public only to perform unit tests.
    //InstrumentFactoryTests.InstrumentTypeBondRequestReturnsBondType
    
    public class Bond : Instrument
    {
        // tolerance can me made a configurable item in app.config, if necessary
        private const int tolerance = 100000;
        
        public override int Tolerance => tolerance;
        public override decimal TransactionCost => MarketValue * (decimal) (2 / (double)100);

        public Bond() : base(InstrumentTypeEnum.Bond)
        {
        }
    }
}
