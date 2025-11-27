using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeFormTests.PageObjects
{
    public class AutomationPracticeFormPage
    {
        private readonly IWebDriver _driver;
        private readonly ResilientElementLocator _locator;
        private readonly WebDriverWait _wait;

        private const string PageUrl = "https://app.cloudqa.io/home/AutomationPracticeForm";

        public AutomationPracticeFormPage(IWebDriver driver)
        {
            _driver = driver;
            _locator = new ResilientElementLocator(driver);
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(PageUrl);
        }

        #region First Name Field

        public IWebElement FirstNameField => _locator.FindElement(
            By.Id("fname"),
            By.Name("fname"),
            By.XPath("//input[@placeholder='First Name']"),
            By.XPath("//label[contains(text(), 'First Name')]/following::input[1]"),
            By.XPath("(//input[@type='text'])[1]"),
            By.CssSelector("input[placeholder*='First']"));

        public void EnterFirstName(string firstName)
        {
            var field = FirstNameField;
            field.Clear();
            field.SendKeys(firstName);
        }

        public string GetFirstNameValue()
        {
            return FirstNameField.GetAttribute("value");
        }

        #endregion

        #region Email Field

        public IWebElement EmailField => _locator.FindElement(
            By.Id("email"),
            By.Name("email"),
            By.XPath("//input[@type='email']"),
            By.XPath("//input[@placeholder='Email']"),
            By.XPath("//input[contains(@placeholder, 'mail')]"),
            By.XPath("//label[contains(text(), 'Email')]/following::input[1]"),
            By.XPath("//label[contains(text(), 'Email')]/..//input"),
            By.CssSelector("input[type='email']"),
            By.CssSelector("input[name*='email' i]")
        );

        public void EnterEmail(string email)
        {
            var field = EmailField;
            field.Clear();
            field.SendKeys(email);
        }

        public string GetEmailValue()
        {
            return EmailField.GetAttribute("value");
        }

        #endregion

        #region Mobile Number Field

        public IWebElement MobileNumberField => _locator.FindElement(
            By.Id("phone"),
            By.Id("mobile"),
            By.Name("phone"),
            By.Name("mobile"),
            By.XPath("//input[@placeholder='Mobile #']"),
            By.XPath("//input[contains(@placeholder, 'Mobile')]"),
            By.XPath("//input[contains(@placeholder, 'Phone')]"),
            By.XPath("//label[contains(text(), 'Mobile')]/following::input[1]"),
            By.XPath("//label[contains(text(), 'Mobile')]/..//input"),
            By.CssSelector("input[type='tel']"),
            By.XPath("//input[@type='text' and (contains(@name, 'phone') or contains(@name, 'mobile'))]"),
            By.CssSelector("input[aria-label*='mobile' i]"),
            By.CssSelector("input[aria-label*='phone' i]")
        );

        public void EnterMobileNumber(string mobileNumber)
        {
            var field = MobileNumberField;
            field.Clear();
            field.SendKeys(mobileNumber);
        }

        public string GetMobileNumberValue()
        {
            return MobileNumberField.GetAttribute("value");
        }

        #endregion

        #region Helper Methods

        public bool IsLoaded()
        {
            try
            {
                _wait.Until(driver => 
                    driver.FindElement(By.TagName("form")) != null ||
                    driver.FindElement(By.XPath("//h2[contains(text(), 'Form')]")) != null
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void WaitForPageLoad()
        {
            _wait.Until(driver => ((IJavaScriptExecutor)driver)
                .ExecuteScript("return document.readyState").Equals("complete"));
        }

        #endregion
    }
}
