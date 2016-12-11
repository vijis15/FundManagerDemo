using FundManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundManagerTests.Model
{
    [TestClass()]
    public class BondTests
    {
        [TestMethod()]
        public void BondIsIntolerantWhenTransactionCostGreaterThanTolerance()
        {
            //Tolerance for Bond = 100,000
            //Note that there are no setters for Market Value, Tolerance since these are 
            //derived from Price and Quantity and has to be tested by manipulating these numbers
            var testBond = InstrumentFactory.CreateStock(InstrumentTypeEnum.Bond);
            testBond.Price = 5000;
            testBond.Quantity = 1001;
            var isTolerant = testBond.IsInstrumentTolerant;
            Assert.IsFalse(isTolerant);
        }

        //Other tests can be added where Tolerance = 100,000 and Tolerance < 100,000 
        //and/or Market Value < 0.
    }
}