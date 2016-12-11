using System;
using FundManager.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundManagerTests.ViewModel
{
    //Calculation of Weights is tested as part of testing the FundManagerCalculationsService

    [TestClass()]
    public class FundManagerViewModelTests
    {
        [TestMethod()]
        public void CanExecuteReturnsFalseWhenPriceAndQuantityIsNotSet()
        {
            FundManagerViewModel viewModel = new FundManagerViewModel();
            bool canExecute = viewModel.AddInstrumentCommand.CanExecute(null);
            Assert.IsFalse(canExecute);
        }

        [TestMethod()]
        public void CanExecuteReturnsFalseWhenPriceIsSetAndQuantityNotSet()
        {
            FundManagerViewModel viewModel = new FundManagerViewModel();
            viewModel.Price = "200";
            viewModel.Quantity = string.Empty;
            bool canExecute = viewModel.AddInstrumentCommand.CanExecute(null);
            Assert.IsFalse(canExecute);
        }

        [TestMethod()]
        public void CanExecuteReturnsFalseWhenQuantityIsSetAndPriceNotSet()
        {
            FundManagerViewModel viewModel = new FundManagerViewModel
            {
                Price = string.Empty,
                Quantity = "200"
            };
            bool canExecute = viewModel.AddInstrumentCommand.CanExecute(null);
            Assert.IsFalse(canExecute);
        }

        [TestMethod()]
        public void CanExecuteReturnsTrueWhenPriceAndQuantityIsSet()
        {
            FundManagerViewModel viewModel = new FundManagerViewModel
            {
                Price = "200",
                Quantity = "400"
            };
            bool canExecute = viewModel.AddInstrumentCommand.CanExecute(null);
            Assert.IsTrue(canExecute);
        }

        [TestMethod()]
        public void ValidateFailsWhenPriceIsNonNumeric()
        {
            FundManagerViewModel viewModel = new FundManagerViewModel {Price = "abc200"};
            string error = viewModel.Validate("Price");
            Assert.IsNotNull(error);
        }

        [TestMethod()]
        public void ValidateFailsWhenQuantityIsNonNumeric()
        {
            FundManagerViewModel viewModel = new FundManagerViewModel();
            viewModel.Quantity = "abc200";
            string error = viewModel.Validate("Quantity");
            Assert.IsNotNull(error);
        }

        [TestMethod()]
        public void ValidatePassWhenPriceIsValid()
        {
            FundManagerViewModel viewModel = new FundManagerViewModel {Price = "200.5656"};
            string error = viewModel.Validate("Price");
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod()]
        public void ValidatePassWhenQuantityIsValid()
        {
            FundManagerViewModel viewModel = new FundManagerViewModel {Quantity = "200"};
            string error = viewModel.Validate("Quantity");
            Assert.AreEqual(string.Empty, error);
        }
    }
}