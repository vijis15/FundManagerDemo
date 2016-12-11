using System;
using FundManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundManagerTests.Model
{
    [TestClass()]
    public class InstrumentFactoryTests
    {
        [TestMethod()]
        public void InstrumentTypeBondRequestReturnsBondType()
        {
            var testBond = InstrumentFactory.CreateStock(InstrumentTypeEnum.Bond);
            Assert.IsInstanceOfType(testBond, typeof(Bond));
        }

        [TestMethod()]
        public void InstrumentTypeEquityRequestReturnsEquityType()
        {
            var testEquity = InstrumentFactory.CreateStock(InstrumentTypeEnum.Equity);
            Assert.IsInstanceOfType(testEquity, typeof(Equity));
        }
    }
}
