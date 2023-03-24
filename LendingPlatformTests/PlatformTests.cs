using LendingPlatform;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NuGet.Frameworks;
using System.Net.WebSockets;

namespace PlatformTest
{
    [TestClass]
    public class CheckApplicationTests
    {
        #region Fail Test
        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_AllEmpty()
        {
            double loanAmountInput = 0;
            int creditScore = 0;
            double ltv = 0;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }
        #endregion

        #region Loan Size
        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LoanTooSmall()
        {
            double loanAmountInput = 99999;
            int creditScore = 999;
            double ltv = 0.1;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LoanTooBig()
        {
            double loanAmountInput = 1500001;
            int creditScore = 999;
            double ltv = 0.1;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }

        #endregion

        #region Loan Over 1m
        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LoanOver1_LTVFail()
        {
            double loanAmountInput = 1000000;
            int creditScore = 950;
            double ltv = 0.6;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LoanOver1_CSFail()
        {
            double loanAmountInput = 1000000;
            int creditScore = 949;
            double ltv = 0.59999;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LoanOver1()
        {
            double loanAmountInput = 1000000;
            int creditScore = 950;
            double ltv = 0.59999;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsTrue(actual);
        }
        #endregion

        #region 60 LTV
        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LTV60Fail()
        {
            double loanAmountInput = 500000;
            int creditScore = 749;
            double ltv = 0.59999;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LTV60Pass()
        {
            double loanAmountInput = 500000;
            int creditScore = 750;
            double ltv = 0.59999;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsTrue(actual);
        }

        #endregion

        #region 80 LTV
        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LTV80Fail()
        {
            double loanAmountInput = 500000;
            int creditScore = 799;
            double ltv = 0.79999;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LTV80Pass()
        {
            double loanAmountInput = 500000;
            int creditScore = 800;
            double ltv = 0.79999;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsTrue(actual);
        }

        #endregion

        #region 90 LTV
        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LTV90Fail()
        {
            double loanAmountInput = 500000;
            int creditScore = 899;
            double ltv = 0.89999;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LTV90Pass()
        {
            double loanAmountInput = 500000;
            int creditScore = 900;
            double ltv = 0.89999;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsTrue(actual);
        }

        #endregion

        #region over 90 ltv
        [TestMethod]
        public void LendingPlatform_Platform_CheckApplication_LTVOver90Fail()
        {
            double loanAmountInput = 500000;
            int creditScore = 999;
            double ltv = 0.90;

            var actual = Platform.CheckApplication(loanAmountInput, creditScore, ltv);

            Assert.IsFalse(actual);
        }

        #endregion
    }
}