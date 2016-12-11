using FundManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundManagerTests.Model
{
    [TestClass()]
    public class InstrumentTests
    {
        [TestMethod()]
        public void MarketValueIsProductOfPriceAndQuantity()
        {
            //Can create either bond or equity as the formula is same for both
            var testBond = InstrumentFactory.CreateStock(InstrumentTypeEnum.Bond);
            testBond.Price = 10;
            testBond.Quantity = 10;
            Assert.AreEqual(100, testBond.MarketValue);
        }
    }
}
