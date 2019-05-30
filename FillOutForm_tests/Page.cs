using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.PageObjects;

//All web elements located on the page fill-out-form with locators and methods

namespace FillOutForm_tests
{
    class FillOutForm
    {
        private IWebDriver Driver { get; set; }

        //Constuctor
        public FillOutForm(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(20)));

        }



        //Locators
        [FindsBy(How = How.Id, Using = "et_pb_contact_name_1")]
        public IWebElement NameField { get; set; }

        [FindsBy(How = How.Id, Using = "et_pb_contact_message_1")]
        public IWebElement MessageField { get; set; }
        [FindsBy(How = How.ClassName, Using = "et_pb_contact_captcha_question")]
        public IWebElement SumExpressionField { get; set; }
        [FindsBy(How = How.XPath, Using = "//div [@class = 'et_pb_column et_pb_column_1_2 et_pb_column_1    et_pb_css_mix_blend_mode_passthrough']//button")]
        public IWebElement SubmitButton { get; set; }
        [FindsBy(How = How.Name, Using = "et_pb_contact_captcha_1")]
        public IWebElement ResultField { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[@id = 'et_pb_contact_form_1']//div[@class = 'et-pb-contact-message']")]
        public IWebElement SucessErrorField { get; set; }

        public By SuccessErrorFieldBy = By.XPath("//div[@id = 'et_pb_contact_form_1']//div[@class = 'et-pb-contact-message']");


        //Actions on the web elements
        public void FillNameField(String name)
        {
            NameField.SendKeys(name);
        }

        public void FillMessageField(String message)
        {
            MessageField.SendKeys(message);
        }

        public void FillResultField(String result)
        {
            ResultField.SendKeys(result);
        }

        public void ClickOnSubmitButton()
        {
            SubmitButton.Click();
        }

        public String GetSucessErrorMsgText()
        {
            return SucessErrorField.Text;
        }

        public String GetSumExpressionText()
        {
            return SumExpressionField.Text;
        }

        //Method for filling the form
        public void FillOutFormMethod(String name, String message, String result)
        {
            FillNameField(name);
            FillMessageField(message);
            FillResultField(result);
        }

    }

}

