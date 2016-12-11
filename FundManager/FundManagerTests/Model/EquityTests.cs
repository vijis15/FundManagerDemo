using FundManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundManagerTests.Model
{
    [TestClass()]
    public class EquityTests
    {
        [TestMethod()]
        public void EquityIsIntolerantWhenTransactionCostGreaterThanTolerance()
        {
            //Tolerance for Equity = 200,000
            //Note that there are no setters for Market Value, Tolerance since these are 
            //derived from Price and Quantity and has to be tested by manipulating these numbers
            var testEquity = InstrumentFactory.CreateStock(InstrumentTypeEnum.Equity);
            testEquity.Price = 5000;
            testEquity.Quantity = 8001;
            var isTolerant = testEquity.IsInstrumentTolerant;
            Assert.IsFalse(isTolerant);
        }

        //Other tests can be added where Tolerance = 200,000 and Tolerance < 200,000 
        //and/or Market Value < 0.
    }
}