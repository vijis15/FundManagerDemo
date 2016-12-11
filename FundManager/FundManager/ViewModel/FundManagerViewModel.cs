using System;
using System.Windows.Input;
using FundManager.Model;
using System.Collections.ObjectModel;
using System.Reflection;
using FundManager.ViewModel.Services;

namespace FundManager.ViewModel
{
    public class FundManagerViewModel : PropertyChangeNotifier
    {
        public FundManagerViewModel()
        {
            AddInstrumentCommand = new RelayCommand(AddStock, CanExecute);
            InstrumentCollection = new ObservableCollection<IInstrument>();
            InstrumentSummaryCollection = new ObservableCollection<InstrumentSummary>();
        }

        public string Price { get; set; }
        public string Quantity { get; set; }

        public decimal TotalMarketValue { get; set; }

        private const string ErrorMessage = "System Error.Please restart application";
        private string _systemMessage;
        public string SystemMessage
        {
            get
            {
                return _systemMessage;
            }
            set
            {
                _systemMessage = value;
                //Substring because the name is returned as 'set_<name>'
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        public InstrumentTypeEnum InstrumentType { get; set; }

        public bool IsStockTolerant { get; set; }

        public ICommand AddInstrumentCommand { get; }
        private bool CanExecute(object arg)
        {
            try
            {
                return !string.IsNullOrEmpty(Price) && GetPriceValue(Price) > 0 &&
                          !string.IsNullOrEmpty(Quantity) && GetQuantityValue(Quantity) > 0;
            }
            catch (Exception)
            {
                SystemMessage = ErrorMessage;
                return false;
                //Exceptions are to be logged
            }
        }

        public ObservableCollection<IInstrument> InstrumentCollection { get; set; }

        public ObservableCollection<InstrumentSummary> InstrumentSummaryCollection { get; set; }

        public override string Validate(string columnName)
        {
            try
            {
                string errorMessage = string.Empty;
                switch (columnName)
                {
                    case "Price":
                        if (string.IsNullOrEmpty(Price) || GetPriceValue(Price) <= 0)
                        {
                            errorMessage = "Value should be a positive integer, greater than 0";
                        }
                        break;
                    case "Quantity":
                        if (string.IsNullOrEmpty(Quantity) || GetQuantityValue(Quantity) <= 0)
                        {
                            errorMessage = "Value should be decimal, greater than 0";
                        }
                        break;
                }
                return errorMessage;
            }
            catch (Exception)
            {
                SystemMessage = ErrorMessage;
                return String.Empty;
                //Exceptions are to be logged
            }
        }

        private void AddStock(object obj)
        {
            try
            {
                IInstrument stock = InstrumentFactory.CreateStock(InstrumentType);
                if (stock != null)
                {
                    // No explicit null check required for Price and Quantity as the CanExecute Method handles this
                    stock.InstrumentType = InstrumentType;
                    stock.Price = GetPriceValue(Price);
                    stock.Quantity = GetQuantityValue(Quantity);

                    //One can keep a separate stock count for Equity and Bond as an alternative.
                    //This approach, however will ensure that there will be no code changes required if we add a new instrument/stock type
                    int stockCount = 0;
                    foreach (var item in InstrumentCollection)
                    {
                        if (item.InstrumentType == stock.InstrumentType)
                        {
                            stockCount++;
                        }
                    }
                    stockCount++;
                    stock.Name = $"{stock.InstrumentType}{stockCount}";
                    InstrumentCollection.Add(stock);

                    //The service can also be injected as a dependency
                    FundManagerCalculationsService.GetServiceInstance().ReviseStockWeights(InstrumentCollection);
                    FundManagerCalculationsService.GetServiceInstance().ReviseSummary(InstrumentCollection, InstrumentSummaryCollection);
                }
            }
            catch (Exception)
            {
                SystemMessage = ErrorMessage;
                //Exceptions are to be logged
            }
        }

        //Price is a string property on the UI to support better validation and to override a limitaion with 
        //UpdateSourceTrigger=PropertyChanged property which was not triggering correctly if user deletes the contents
        private decimal GetPriceValue(string inPrice)
        {
            try
            {
                decimal price;
                decimal.TryParse(inPrice, out price);
                return price;
            }
            catch (Exception)
            {
                SystemMessage = ErrorMessage;
                return 0;
                //Exceptions are to be logged
            }
        }

        //Quantity is a string property on the UI to support better validation and to override a limitaion with 
        //UpdateSourceTrigger=PropertyChanged property which was not triggering correctly if user deletes the contents
        private int GetQuantityValue(string inQuantity)
        {
            try
            {
                int quantity;
                int.TryParse(inQuantity, out quantity);
                return quantity;
            }
            catch (Exception)
            {
                SystemMessage = ErrorMessage;
                return 0;
                //Exceptions are to be logged
            }
        }
    }
}
