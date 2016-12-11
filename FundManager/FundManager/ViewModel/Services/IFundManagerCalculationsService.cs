using System.Collections.Generic;
using System.Collections.ObjectModel;
using FundManager.Model;

namespace FundManager.ViewModel.Services
{
    public interface IFundManagerCalculationsService
    {
        void ReviseStockWeights(IEnumerable<IInstrument> instruments);

        void ReviseSummary(Collection<IInstrument> instrumentCollection,
            Collection<InstrumentSummary> instrumentSummaryCollection);
    }
}