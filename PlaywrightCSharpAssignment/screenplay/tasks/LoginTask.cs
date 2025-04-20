using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightCSharpAssignment.screenplay.tasks
{
    public class LoginTask : ITask
    {
        private readonly string _email;
        private readonly string _password;

        private LoginTask(string email, string password) // <--- private constructor
        {
            _email = email;
            _password = password;
        }

        public static LoginTask WithDetails(string email, string password) // <--- static method
        {
            return new LoginTask(email, password);
        }

        public async Task PerformAs(Actor actor)
        {
            var page = actor.Page;

            await page.Locator("input[data-qa='login-email']").FillAsync(_email);
            await page.Locator("input[data-qa='login-password']").FillAsync(_password);
            await page.Locator("button[data-qa='login-button']").ClickAsync();
        }
    }
}