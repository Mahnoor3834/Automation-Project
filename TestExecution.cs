using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using AventStack.ExtentReports.Model;

namespace STPROJECTFINAL
{
    [TestClass]
    public class TestExecution : BasePage
    {
        #region Setups and Cleanups
        public TestContext instance;
        public TestContext TestContext
        {
            set { instance = value; }
            get { return instance; }
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            string ResultFile = @"C:\Users\smahn\source\repos\STPROJECTFINAL\ExtentReports\TestExecLog_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            CreateReport(ResultFile);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            extentReports.Flush();
        }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {

        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
        }

        [TestInitialize()]
        public void TestInit()
        {
            BasePage.SeleniumInit(ConfigurationManager.AppSettings["Browser"].ToString());
            Test = extentReports.CreateTest(TestContext.TestName);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            BasePage.driver.Close();
        }

        #endregion
        [TestMethod]
        public void TestMethod1()
        {

        }
    }

}
