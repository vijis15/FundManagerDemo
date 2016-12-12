using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FundManager.Model;
using FundManager.ViewModel;
using FundManager.ViewModel.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundManagerTests.ViewModel.Services
{
    [TestClass()]
    public class FundManagerCalculationsServiceTests
    {
        [TestMethod()]
        public void OneFundReturnsWeightAs100()
        {
            var testInstruments = new List<IInstrument>();
            //Add 1 bond.
            //When there is only 1 instrument, weight should be 100
            var testBond = InstrumentFactory.CreateStock(InstrumentTypeEnum.Bond);
            //Price and Quantity is necessary since it is necessary to derive Market Value
            //Market Value is needed for weight calculation
            testBond.Price = 10;
            testBond.Quantity = 10;
            testInstruments.Add(testBond);
            FundManagerCalculationsService.GetServiceInstance().ReviseStockWeights(testInstruments);
            var firstInstrument = testInstruments.FirstOrDefault();
            if (firstInstrument != null) Assert.AreEqual(100, firstInstrument.Weight);
        }

        [TestMethod()]
        public void TwoFundReturnsWeightAs50()
        {
            var testInstruments = new List<IInstrument>();
            //Add 2 bond.
            //When there is 2 instruments, weight should be 50
            var testBond = InstrumentFactory.CreateStock(InstrumentTypeEnum.Bond);
            //Price and Quantity is necessary since it is necessary to derive Market Value
            //Market Value is needed for weight calculation
            testBond.Price = 10;
            testBond.Quantity = 10;
            testInstruments.Add(testBond);

            var testEquity = InstrumentFactory.CreateStock(InstrumentTypeEnum.Equity);
            //Price and Quantity is necessary since it is necessary to derive Market Value
            //Market Value is needed for weight calculation
            testEquity.Price = 10;
            testEquity.Quantity = 10;
            testInstruments.Add(testEquity);

            FundManagerCalculationsService.GetServiceInstance().ReviseStockWeights(testInstruments);
            var firstInstrument = testInstruments.FirstOrDefault();
            if (firstInstrument != null) Assert.AreEqual(50, firstInstrument.Weight);
        }

        [TestMethod()]
        public void ReviseSummaryTest()
        {
            var instrumentSummaryCollection = new Collection<InstrumentSummary>();

            var testInstruments = new Collection<IInstrument>();
            //Add 2 bonds
            var testBond1 = InstrumentFactory.CreateStock(InstrumentTypeEnum.Bond);
            //Price and Quantity is necessary since it is necessary to derive Market Value
            //Market Value is needed for weight calculation
            testBond1.Price = 10;
            testBond1.Quantity = 10;
            testInstruments.Add(testBond1);

            var testBond2 = InstrumentFactory.CreateStock(InstrumentTypeEnum.Bond);
            //Price and Quantity is necessary since it is necessary to derive Market Value
            //Market Value is needed for weight calculation
            testBond2.Price = 10;
            testBond2.Quantity = 10;
            testInstruments.Add(testBond2);

            //Add 2 Equity funds
            var testEquity1 = InstrumentFactory.CreateStock(InstrumentTypeEnum.Equity);
            //Price and Quantity is necessary since it is necessary to derive Market Value
            //Market Value is needed for weight calculation
            testEquity1.Price = 10;
            testEquity1.Quantity = 10;
            testInstruments.Add(testEquity1);

            var testEquity2 = InstrumentFactory.CreateStock(InstrumentTypeEnum.Equity);
            //Price and Quantity is necessary since it is necessary to derive Market Value
            //Market Value is needed for weight calculation
            testEquity2.Price = 10;
            testEquity2.Quantity = 10;
            testInstruments.Add(testEquity2);

            FundManagerCalculationsService.GetServiceInstance().ReviseStockWeights(testInstruments);

            FundManagerCalculationsService.GetServiceInstance().ReviseSummary
                (testInstruments, instrumentSummaryCollection);

            foreach (var item in instrumentSummaryCollection)
            {
                switch (item.InstrumentType)
                {
                    case InstrumentTypeEnum.Bond:
                        Assert.AreEqual(2, item.TotalNumber);
                        Assert.AreEqual(200, item.TotalMarketValue);
                        Assert.AreEqual(50, item.TotalWeight);
                        break;
                    case InstrumentTypeEnum.Equity:
                        Assert.AreEqual(2, item.TotalNumber);
                        Assert.AreEqual(200, item.TotalMarketValue);
                        Assert.AreEqual(50, item.TotalWeight);
                        break;
                    case InstrumentTypeEnum.Fund:
                        Assert.AreEqual(1, item.TotalNumber);
                        Assert.AreEqual(400, item.TotalMarketValue);
                        Assert.AreEqual(100, item.TotalWeight);
                        break;
                }
            }
        }
    }
}
