using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightCSharpAssignment.utilities
{
    public class BaseTest
    {
        protected IPage Page;
        protected IBrowser Browser;
        protected IBrowserContext? Context;
        protected IPlaywright Playwright;

        [OneTimeSetUp]
        public async Task Setup()
        {
            PlaywrightInstaller.InstallBrowsers();
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // set true when we are reasdy tom integrate with CI pipeline
            });
            Context = await Browser.NewContextAsync();
            Page = await Context.NewPageAsync();
        }

        [OneTimeTearDown]
        public async Task Teardown()
        {
            await Browser.CloseAsync();
            Playwright.Dispose();
        }
    }
}