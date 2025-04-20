using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightCSharpAssignment.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly ILocator _signupNameInput;
        private readonly ILocator _signupEmailInput;
        private readonly ILocator _signupButton;
        private readonly ILocator _loginEmailInput;
        private readonly ILocator _loginPasswordInput;
        private readonly ILocator _loginButton;
        private readonly ILocator _consentButton;
        private readonly ILocator _passwordInput;
        private readonly ILocator _dayInput;
        private readonly ILocator _monthInput;
        private readonly ILocator _yearInput;

        private readonly ILocator _firstNameInput;
        private readonly ILocator _lastNameInput;
        private readonly ILocator _addressFirstLineInput;
        private readonly ILocator _countryDropDown;
        private readonly ILocator _stateInput;
        private readonly ILocator _cityInput;
        private readonly ILocator _zipcodeInput;
        private readonly ILocator _mobileNumberInput;
        private readonly ILocator _genderSelection;
        private readonly ILocator _createAccountButton;
        private readonly ILocator _continueButton;



        public LoginPage(IPage page)
        {
            _page = page;
            _signupNameInput = page.Locator("[data-qa='signup-name']");
            _signupEmailInput = page.Locator("[data-qa='signup-email']");
            _signupButton = page.Locator("[data-qa='signup-button']");
            _loginEmailInput = page.Locator("[data-qa='login-email']");
            _loginPasswordInput = page.Locator("[data-qa='login-password']");
            _loginButton = page.Locator("[data-qa='login-button']");
            _consentButton = page.Locator("button.fc-button.fc-cta-consent.fc-primary-button");
            _passwordInput = page.Locator("[data-qa='password']");
            _createAccountButton = page.Locator("[data-qa='create-account']");



            _dayInput = page.Locator("[data-qa='days']");
            _monthInput = page.Locator("[data-qa='months']");
            _yearInput = page.Locator("[data-qa='years']");

            _genderSelection = page.Locator("[id='id_gender1']");
            _firstNameInput = page.Locator("[data-qa='first_name']");
            _lastNameInput = page.Locator("[data-qa='last_name']");
            _addressFirstLineInput = page.Locator("[data-qa='address']");
            _countryDropDown = page.Locator("[data-qa='country']");
            _stateInput = page.Locator("[data-qa='state']");
            _cityInput = page.Locator("[data-qa='city']");
            _zipcodeInput = page.Locator("[data-qa='zipcode']");
            _mobileNumberInput = page.Locator("[data-qa='mobile_number']");

            _continueButton = page.Locator("[data-qa='continue-button']");
            _consentButton =  page.Locator("div.fc-dialog button:has-text(\"Consent\")");

        }

        public async Task Signup(string name, string email, string password)
        {

            await HandleConsentDialog();      // Use to handle consent flex dialog

            await _signupNameInput.FillAsync(name);
            await _signupEmailInput.FillAsync(email);
            await _signupButton.ClickAsync();

            await _genderSelection.ClickAsync();

            await _passwordInput.FillAsync(password);
            await _dayInput.SelectOptionAsync("4");
            await _monthInput.SelectOptionAsync("March");
            await _yearInput.SelectOptionAsync("1980");

            
            await _firstNameInput.FillAsync("firstName");
            await _lastNameInput.FillAsync("lastName");
            await _addressFirstLineInput.FillAsync("13 rosemary grove");

            await _countryDropDown.SelectOptionAsync("India");
            await _stateInput.FillAsync("Kerala");
            await _cityInput.FillAsync("Munnar");
            await _zipcodeInput.FillAsync("4876565");
            await _mobileNumberInput.FillAsync("987898765");

            await _createAccountButton.ClickAsync();

            Assert.That(await _continueButton.IsVisibleAsync(), Is.True);
            Assert.That(await _page.Locator("p:has-text(\"Congratulations! Your new account has been successfully created!\")").IsVisibleAsync(), Is.True);

            await _continueButton.ClickAsync();

            await _page.GetByRole(AriaRole.Link, new() { Name = "Logout" }).ClickAsync();

        }

        public async Task Login(string email, string password)
        {
            await _loginEmailInput.FillAsync(email);
            await _loginPasswordInput.FillAsync(password);
            await _loginButton.ClickAsync();
        }

        private async Task HandleConsentDialog()
        {
            try
            {
                if (await _consentButton.IsVisibleAsync())
                {
                   await _consentButton.ClickAsync();
                }
                else{
                     // wait for the button to be visible and enabled
                    await _consentButton.WaitForAsync(new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 5000
                    });
                    await _consentButton.ClickAsync();
                }
            }
             catch (TimeoutException ex)
            {
                Console.WriteLine($"Timed out waiting for Consent dialog: {ex.Message}");
                // No rethrow, continue test normally
            }
            catch (PlaywrightException ex)
            {
                Console.WriteLine($"Playwright exception occurred: {ex.Message}");
                //continue test normally
            }

            
        }
    }
}