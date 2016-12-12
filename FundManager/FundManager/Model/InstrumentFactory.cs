namespace FundManager.Model
{
    public static class InstrumentFactory
    {
        public static IInstrument CreateStock(InstrumentTypeEnum stockType)
        {
            IInstrument stock = null; 
            switch (stockType)
            {
                case InstrumentTypeEnum.Equity:
                    stock = new Equity();
                    break;
                case InstrumentTypeEnum.Bond:
                    stock = new Bond();
                    break;
            }
            return stock;
        }
    }
}
