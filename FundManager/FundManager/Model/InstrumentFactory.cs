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
                    stock = new Equity {InstrumentType = InstrumentTypeEnum.Equity};
                    break;
                case InstrumentTypeEnum.Bond:
                    stock = new Bond {InstrumentType = InstrumentTypeEnum.Bond};
                    break;
            }
            return stock;
        }
    }
}
