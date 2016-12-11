using System.Windows.Input;
using FundManager.Model;
using System.Collections.ObjectModel;
using FundManager.ViewModel.Services;

namespace FundManager.ViewModel
{
    public class FundManagerViewModel: PropertyChangeNotifier
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

        public InstrumentTypeEnum InstrumentType { get; set; }

        public bool IsStockTolerant { get; set; }

        public ICommand AddInstrumentCommand { get; }
        private bool CanExecute(object arg)
        {
            decimal quantity;
            decimal.TryParse(Price, out quantity);

            return !string.IsNullOrEmpty(Price) && GetPriceValue(Price) > 0 &&
                  !string.IsNullOrEmpty(Quantity) && GetQuantityValue(Quantity) > 0;
        }

        public ObservableCollection<IInstrument> InstrumentCollection { get; set; }

        public ObservableCollection<InstrumentSummary> InstrumentSummaryCollection { get; set; }

        public override string Validate(string columnName)
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

        private void AddStock(object obj)
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

        //Price is a string property on the UI to support better validation and to override a limitaion with 
        //UpdateSourceTrigger=PropertyChanged property which was not triggering correctly if user deletes the contents
        private decimal GetPriceValue(string inPrice)
        {
            decimal price;
            decimal.TryParse(inPrice, out price);
            return price;
        }

        //Quantity is a string property on the UI to support better validation and to override a limitaion with 
        //UpdateSourceTrigger=PropertyChanged property which was not triggering correctly if user deletes the contents
        private int GetQuantityValue(string inQuantity)
        {
            int quantity;
            int.TryParse(inQuantity, out quantity);
            return quantity;
        }
    }
}
