using System.Reflection;

namespace FundManager.Model
{
    public abstract class Instrument: ViewModel.PropertyChangeNotifier, IInstrument
    {
        public InstrumentTypeEnum InstrumentType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal MarketValue => Price*Quantity;

        private decimal _weight;
        public decimal Weight {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                //Substring because the name is returned as 'set_<name>'
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        public abstract int Tolerance { get; }
        public abstract decimal TransactionCost { get; }

        public bool IsInstrumentTolerant => !(MarketValue < 0 || TransactionCost > Tolerance);
    }
}
