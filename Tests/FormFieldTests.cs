using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using AutomationPracticeFormTests.PageObjects;

namespace AutomationPracticeFormTests.Tests
{
    [TestFixture]
    public class FormFieldTests
    {
        private IWebDriver? _driver;
        private AutomationPracticeFormPage? _formPage;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");
            
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            _formPage = new AutomationPracticeFormPage(_driver);
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }

        #region First Name Field Tests

        [Test]
        [Category("FirstName")]
        [Description("Test that First Name field accepts valid text input")]
        public void FirstNameField_WithValidInput_ShouldAcceptText()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            string expectedName = "John";

            _formPage.EnterFirstName(expectedName);
            string actualName = _formPage.GetFirstNameValue();

            Assert.That(actualName, Is.EqualTo(expectedName), 
                "First Name field should contain the entered value");
        }

        [Test]
        [Category("FirstName")]
        [Description("Test that First Name field can be cleared and updated")]
        public void FirstNameField_WhenCleared_ShouldAcceptNewValue()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            string initialName = "Jane";
            string updatedName = "Sarah";

            _formPage.EnterFirstName(initialName);
            Assert.That(_formPage.GetFirstNameValue(), Is.EqualTo(initialName), 
                "Initial value should be set");

            _formPage.EnterFirstName(updatedName);
            string finalName = _formPage.GetFirstNameValue();

            Assert.That(finalName, Is.EqualTo(updatedName), 
                "First Name field should contain the updated value");
            Assert.That(finalName, Is.Not.EqualTo(initialName), 
                "Old value should be replaced");
        }

        [Test]
        [Category("FirstName")]
        [Description("Test that First Name field accepts special characters and spaces")]
        public void FirstNameField_WithSpecialCharacters_ShouldAcceptInput()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            string nameWithSpecialChars = "Mary-Anne O'Brien";

            _formPage.EnterFirstName(nameWithSpecialChars);
            string actualName = _formPage.GetFirstNameValue();

            Assert.That(actualName, Is.EqualTo(nameWithSpecialChars), 
                "First Name field should accept special characters like hyphens and apostrophes");
        }

        #endregion

        #region Email Field Tests

        [Test]
        [Category("Email")]
        [Description("Test that Email field accepts valid email format")]
        public void EmailField_WithValidEmail_ShouldAcceptInput()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            string validEmail = "test.user@example.com";

            _formPage.EnterEmail(validEmail);
            string actualEmail = _formPage.GetEmailValue();

            Assert.That(actualEmail, Is.EqualTo(validEmail), 
                "Email field should contain the entered email address");
        }

        [Test]
        [Category("Email")]
        [Description("Test that Email field can handle different email formats")]
        public void EmailField_WithDifferentFormats_ShouldAcceptValidEmails()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            
            var testEmails = new[]
            {
                "simple@example.com",
                "firstname.lastname@example.com",
                "email@subdomain.example.com",
                "firstname+lastname@example.com"
            };

            foreach (var email in testEmails)
            {
                _formPage.EnterEmail(email);
                string actualEmail = _formPage.GetEmailValue();
                
                Assert.That(actualEmail, Is.EqualTo(email), 
                    $"Email field should accept the format: {email}");
            }
        }

        [Test]
        [Category("Email")]
        [Description("Test that Email field value persists and can be updated")]
        public void EmailField_WhenUpdated_ShouldReplaceOldValue()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            string oldEmail = "old.email@test.com";
            string newEmail = "new.email@test.com";

            _formPage.EnterEmail(oldEmail);
            Assert.That(_formPage.GetEmailValue(), Is.EqualTo(oldEmail));

            _formPage.EnterEmail(newEmail);
            string finalEmail = _formPage.GetEmailValue();

            Assert.That(finalEmail, Is.EqualTo(newEmail), 
                "Email field should contain the new email");
            Assert.That(finalEmail, Is.Not.EqualTo(oldEmail), 
                "Old email should be replaced");
        }

        #endregion

        #region Mobile Number Field Tests

        [Test]
        [Category("MobileNumber")]
        [Description("Test that Mobile Number field accepts numeric input")]
        public void MobileNumberField_WithValidNumber_ShouldAcceptInput()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            string validMobile = "9876543210";

            _formPage.EnterMobileNumber(validMobile);
            string actualMobile = _formPage.GetMobileNumberValue();

            Assert.That(actualMobile, Is.EqualTo(validMobile), 
                "Mobile Number field should contain the entered number");
        }

        [Test]
        [Category("MobileNumber")]
        [Description("Test that Mobile Number field accepts 10-digit numbers")]
        public void MobileNumberField_WithTenDigits_ShouldAcceptInput()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            string tenDigitNumber = "5551234567";

            _formPage.EnterMobileNumber(tenDigitNumber);
            string actualMobile = _formPage.GetMobileNumberValue();

            Assert.That(actualMobile, Is.EqualTo(tenDigitNumber), 
                "Mobile Number field should accept 10-digit phone numbers");
        }

        [Test]
        [Category("MobileNumber")]
        [Description("Test that Mobile Number field can be cleared and updated")]
        public void MobileNumberField_WhenUpdated_ShouldReplaceOldValue()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            string oldNumber = "1234567890";
            string newNumber = "9876543210";

            _formPage.EnterMobileNumber(oldNumber);
            Assert.That(_formPage.GetMobileNumberValue(), Is.EqualTo(oldNumber));

            _formPage.EnterMobileNumber(newNumber);
            string finalNumber = _formPage.GetMobileNumberValue();

            Assert.That(finalNumber, Is.EqualTo(newNumber), 
                "Mobile Number field should contain the new number");
            Assert.That(finalNumber, Is.Not.EqualTo(oldNumber), 
                "Old number should be replaced");
        }

        #endregion

        #region Integration Tests

        [Test]
        [Category("Integration")]
        [Description("Test that multiple fields can be filled in a single session")]
        public void FormFields_WhenFillingMultipleFields_ShouldRetainAllValues()
        {
            _formPage!.NavigateTo();
            _formPage.WaitForPageLoad();
            
            string firstName = "John";
            string email = "john.doe@example.com";
            string mobile = "9876543210";

            _formPage.EnterFirstName(firstName);
            _formPage.EnterEmail(email);
            _formPage.EnterMobileNumber(mobile);

            Assert.Multiple(() =>
            {
                Assert.That(_formPage.GetFirstNameValue(), Is.EqualTo(firstName), 
                    "First Name should be retained");
                Assert.That(_formPage.GetEmailValue(), Is.EqualTo(email), 
                    "Email should be retained");
                Assert.That(_formPage.GetMobileNumberValue(), Is.EqualTo(mobile), 
                    "Mobile Number should be retained");
            });
        }

        #endregion
    }
}
