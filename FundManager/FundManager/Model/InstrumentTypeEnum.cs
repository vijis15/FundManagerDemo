namespace FundManager.Model
{
    //Adding a new instrument type will invlove setting up the enum here and adding a 
    //new instrument class which will derive from 'Instrument'
    public enum InstrumentTypeEnum
    {
        Equity,
        Bond,
        Fund
    }
}
