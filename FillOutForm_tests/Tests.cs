using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

//This class consist of tests. Also consist setUp and tearDown methods which are 
//used to set up test with necessarily preconditions which will be executed before each test and to set up conditions
//which will be executed after each test

namespace FillOutForm_tests
{
    [TestClass]
    public class AutomationTests
    {
        private IWebDriver Driver { get; set; }
        private FillOutForm FillOutFormPage { get; set; }
        
        private IWebDriver GetChromeDriver()
        {
            var ouputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(ouputDirectory);
        }

        //Necessary preconditions executed before every test
        [TestInitialize]
        public void SetUp()
        {
            Driver = GetChromeDriver();
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");
            Driver.Manage().Window.Maximize();
            FillOutFormPage = new FillOutForm(Driver);
        }

        //Code executed after every test
        [TestCleanup]
        public void TearDown()
        {
            Driver.Close();
            Driver.Quit();
        }

        //This test will fill out form but in the 'Result' field will pass negative 1 which will result in the error msg. 
        //At the end it verifies that sum expression before clicking on the 'Submit' button is different that sum expression
        //after clicking on the 'Submit button'

        [TestMethod]

        public void Test_verifySumExpressionsAreNotEqual()
        {
            Console.WriteLine("*********Running test Verify Sum Expressions Are Not Equal: "+ "\n");
            FillOutFormPage.FillOutFormMethod("Test name", "Test message", "-1");
            var sumExpressionBeforeSubmit = FillOutFormPage.GetSumExpressionText();
            Console.WriteLine("Sum expression before submit is " + "'" + sumExpressionBeforeSubmit + "'");
            FillOutFormPage.ClickOnSubmitButton();
            var sumExpressionAfterSubmit = FillOutFormPage.GetSumExpressionText();
            Console.WriteLine("Sum expression before submit is " + "'" + sumExpressionAfterSubmit + "'");
            Assert.AreNotEqual(sumExpressionBeforeSubmit, sumExpressionAfterSubmit, "Sum expressions are equal!!!");


        }

        //This test will fill out form and it will fill out correct 'Result' which will result showing the success msg to
        //the user.At the end it verifies that success msg is equal to 'Success'

        [TestMethod]
        public void Test_verifySuccessMsg()
        {
            Console.WriteLine("*********Running test Verify Success Msg: "+ "\n");
            var sumExpressionBeforeSubmit = FillOutFormPage.GetSumExpressionText();
            Console.WriteLine("Sum expression before submit is " + "'" + sumExpressionBeforeSubmit + "'");
            var correctResult = Calculation.Addition(sumExpressionBeforeSubmit);
            Console.WriteLine("Correct result is " + correctResult.ToString());
            FillOutFormPage.FillOutFormMethod("Test name", "Test message", correctResult.ToString());
            FillOutFormPage.ClickOnSubmitButton();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(FillOutFormPage.SuccessErrorFieldBy));
            Console.WriteLine("Success msg is " + "'" + FillOutFormPage.GetSucessErrorMsgText() + "'\n");
            Assert.AreEqual("Success", FillOutFormPage.GetSucessErrorMsgText());
        }
       
    }
}
