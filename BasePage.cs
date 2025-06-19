using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.IO;

namespace STPROJECTFINAL
{
    public class BasePage
    {
        public static IWebDriver driver;
        public static ExtentReports extentReports;
        public static ExtentTest Test;
        public static ExtentTest Step;

        public static void SeleniumInit(string browser)
        {
            if (string.IsNullOrEmpty(browser))
            {
                throw new ArgumentNullException(nameof(browser), "Browser must be specified in App.config.");
            }

            if (browser == "Chrome")
            {
                var options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                driver = new ChromeDriver(options);
            }
            else if (browser == "FireFox")
            {
                FirefoxOptions options = new FirefoxOptions();
                options.AddArguments("");
                driver = new FirefoxDriver(options);
            }
            else if (browser == "MicrosoftEdge")
            {
                var options = new EdgeOptions();
                var service = EdgeDriverService.CreateDefaultService(@"D:\fold\Semester\S7\ST\STProject\bin\Debug\", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe");
                service.Start();
                driver = new RemoteWebDriver(service.ServiceUrl, options);
            }
        }

        public void Write(By by, string data)
        {
            try
            {
                driver.FindElement(by).SendKeys(data);
                TakeScreenshot(Status.Pass, data+ "Data Entered Successfully");
            }
            catch (Exception ex)
            {
                TakeScreenshot(Status.Fail, "Failed to enter data" + ex );
            }
            
        }

        public void Click(By by)
        {
            try
            {
                driver.FindElement(by).Click();
                TakeScreenshot(Status.Pass, "Clicked Successfully");
            }
            catch (Exception ex)
            {
                TakeScreenshot(Status.Fail, "Failed to click" + ex);
            }
        }

        public void OpenURL(string url)
        {
            driver.Url = url;
        }

        public IWebElement WaitForElement(By by, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(driver => driver.FindElement(by));
        }

        public IWebElement FindElement(By by)
        {
            return driver.FindElement(by);
        }

        public bool IsElementDisplayed(By locator)
        {
            try
            {
                return driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string ReadText(By locator)
        {
            try
            {
                return driver.FindElement(locator).Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        public static void CreateReport(String path)
        {
            extentReports = new ExtentReports();
            var sparkReporter = new ExtentSparkReporter(path);
            extentReports.AttachReporter(sparkReporter);
        }

        public static void TakeScreenshot(Status status, string stepDetail)
        {
            string path = @"C:\Users\smahn\source\repos\STPROJECTFINAL\ExtentReports\images\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            File.WriteAllBytes(path, screenshot.AsByteArray);

            Step.Log(status, stepDetail, MediaEntityBuilder.CreateScreenCaptureFromPath(path).Build());
        }

    }
}
