using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.Bank.Service;

namespace Sample.Bank.Test
{
    [TestClass]
    public class AccountNumberTests
    {
        private AccountNumber acnSut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentErrorExceptionTest()
        {
            acnSut = new AccountNumber("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ArgumentOutOfRangeExceptionTest()
        {
            acnSut = new AccountNumber("123456-123456568");
        }

        [TestMethod]
        public void CommercialBankAccountNumberTest()
        {
            Assert.AreEqual("12345600000785", new AccountNumber("123456-785").getLongFormat());
        }

        [TestMethod]
        public void CommercialBankAccountNumberFourDigitTest()
        {
            Assert.AreEqual("12345600001783", new AccountNumber("123456-1783").getLongFormat());
        }

        [TestMethod]
        public void SavingsBankAccountNumberTest()
        {
            Assert.AreEqual("42345670000081", new AccountNumber("423456-781").getLongFormat());
        }

        [TestMethod]
        public void SavingsBankAccountNumberLastFourDigitsTest()
        {
            Assert.AreEqual("42345650000788", new AccountNumber("423456-5788").getLongFormat());
        }

        [TestMethod]
        public void SavingsBankAccountNumberLastEightDigitsTest()
        {
            Assert.AreEqual("42345657893458", new AccountNumber("423456-57893458").getLongFormat());
        }

        [TestMethod]
        public void SavingsBankAccountNumberLastTwoDigitsTest()
        {
            Assert.AreEqual("42345650000002", new AccountNumber("423456-52").getLongFormat());
        }
    }
}
