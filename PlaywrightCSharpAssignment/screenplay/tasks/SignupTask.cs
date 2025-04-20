using Microsoft.Playwright;
using PlaywrightCSharpAssignment.Pages;

namespace PlaywrightCSharpAssignment.screenplay.tasks
{
    public class SignupTask : ITask
    {
        private readonly string _username;
        private readonly string _email;
        private readonly string _password;

        public SignupTask(string username, string email, string password)
        {
            _username = username;
            _email = email;
            _password = password;
        }

        public async Task PerformAs(Actor actor)
        {
            var loginPage = new LoginPage(actor.Page);
            await loginPage.Signup(_username, _email, _password);
        }

        public static SignupTask WithDetails(string username, string email, string password)
            => new SignupTask(username, email, password);
    }
}