using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FundManager.Model;

namespace FundManager.ViewModel.Services
{
    /// <summary>
    /// This service can also be injected into the view model as a dependency
    /// Using a GetInstance() approach in this case
    /// </summary>
    public class FundManagerCalculationsService: IFundManagerCalculationsService
    {
        private FundManagerCalculationsService(){}

        private static FundManagerCalculationsService _fundManagerCalculationsService;

        public static IFundManagerCalculationsService GetServiceInstance()
        {
            return _fundManagerCalculationsService ??
                   (_fundManagerCalculationsService = new FundManagerCalculationsService());
        }

        public void ReviseStockWeights(IEnumerable<IInstrument> instruments)
        {
            try
            {
                var totalMarketValue = instruments.Sum(item => item.MarketValue);
                foreach (var item in instruments)
                {
                    item.Weight = (100 * item.MarketValue) / totalMarketValue;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void ReviseSummary(Collection<IInstrument> instrumentCollection,
            Collection<InstrumentSummary> instrumentSummaryCollection)
        {
            //Clearing the older values as the Summary changes upon adding every new stock/instrument
            try
            {
                instrumentSummaryCollection.Clear();

                //Summary at instrument level
                //Adding a new instrument type will not need any code changes here.
                var instrumentsGroupedByType = instrumentCollection.GroupBy(instrument => instrument.InstrumentType);
                foreach (var group in instrumentsGroupedByType)
                {
                    InstrumentSummary instrumentSummary = new InstrumentSummary { InstrumentType = @group.Key };
                    foreach (var groupedItem in group)
                    {
                        instrumentSummary.TotalNumber++;
                        instrumentSummary.TotalMarketValue += groupedItem.MarketValue;
                        instrumentSummary.TotalWeight += groupedItem.Weight;
                    }
                    instrumentSummaryCollection.Add(instrumentSummary);
                }

                //Summary at fund level
                InstrumentSummary fundSummary = new InstrumentSummary
                {
                    InstrumentType = InstrumentTypeEnum.Fund,
                    TotalNumber = 1 //As there is only 1 fund in this demo application
                };
                foreach (var instrumentLevelSummary in instrumentSummaryCollection)
                {
                    fundSummary.TotalMarketValue += instrumentLevelSummary.TotalMarketValue;
                    fundSummary.TotalWeight += instrumentLevelSummary.TotalWeight;
                }
                instrumentSummaryCollection.Add(fundSummary);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
