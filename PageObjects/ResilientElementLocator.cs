using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationPracticeFormTests.PageObjects
{
    public class ResilientElementLocator
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ResilientElementLocator(IWebDriver driver, int timeoutSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        public IWebElement FindElement(params By[] locators)
        {
            foreach (var locator in locators)
            {
                try
                {
                    var element = _wait.Until(driver =>
                    {
                        try
                        {
                            var elem = driver.FindElement(locator);
                            return elem.Displayed ? elem : null;
                        }
                        catch
                        {
                            return null;
                        }
                    });

                    if (element != null)
                        return element;
                }
                catch
                {
                    continue;
                }
            }

            throw new NoSuchElementException($"Could not locate element using any of the provided strategies");
        }

        public IWebElement FindByLabelText(string labelText)
        {
            try
            {
                var label = _driver.FindElement(By.XPath($"//label[normalize-space(text())='{labelText}']"));
                var forAttribute = label.GetAttribute("for");
                if (!string.IsNullOrEmpty(forAttribute))
                {
                    return _driver.FindElement(By.Id(forAttribute));
                }
            }
            catch { }

            try
            {
                return _driver.FindElement(By.XPath($"//label[normalize-space(text())='{labelText}']/following::input[1]"));
            }
            catch { }

            try
            {
                return _driver.FindElement(By.XPath($"//label[normalize-space(text())='{labelText}']/..//input"));
            }
            catch { }

            throw new NoSuchElementException($"Could not locate element with label text: {labelText}");
        }

        public IWebElement FindByPlaceholder(string placeholderText)
        {
            return FindElement(
                By.XPath($"//input[@placeholder='{placeholderText}']"),
                By.XPath($"//*[@placeholder='{placeholderText}']"),
                By.CssSelector($"input[placeholder*='{placeholderText}']")
            );
        }

        public IWebElement FindByTypeAndContext(string type, string contextText)
        {
            return FindElement(
                By.XPath($"//input[@type='{type}' and contains(@placeholder, '{contextText}')]"),
                By.XPath($"//input[@type='{type}' and contains(@name, '{contextText}')]"),
                By.XPath($"//label[contains(text(), '{contextText}')]/following::input[@type='{type}'][1]")
            );
        }
    }
}
