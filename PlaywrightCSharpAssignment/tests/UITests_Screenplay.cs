using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightCSharpAssignment.screenplay;
using PlaywrightCSharpAssignment.screenplay.tasks;
using PlaywrightCSharpAssignment.utilities;
using System.Diagnostics;

namespace PlaywrightCSharpAssignment.tests
{
    [TestFixture]
    [Category("Screenplay")]
    public class UITests_Screenplay : BaseTest
    {
        private Actor _actor;
        private static readonly Random _random = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string Generate(int length)
        {
            return new string(Enumerable.Repeat(_chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string email;
        private string password = "password123";
        private string productSubType = "";

        [SetUp]
        public async Task SetUpActor()
        {
            _actor = new Actor("Meghraj", Page);
            email = Generate(8) + "@mailinator.com";
        }

        [Test, Order(1)]
        public async Task Signup_NewUser()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _actor.Page.GotoAsync("https://automationexercise.com/login");
            await _actor.Page.Locator("button:has-text(\"Consent\")").ClickAsync();

            stopwatch.Stop();
            TestContext.WriteLine($"Page Load Time: {stopwatch.ElapsedMilliseconds} ms");

            await _actor.AttemptsTo(
                SignupTask.WithDetails("TestUser", email, password)
            );

            await ScreenshotHelper.CaptureScreenshot(Page, "signup_screenplay.png");
        }

        [Test, Order(2)]
        public async Task Login_Test()
        {
            await _actor.Page.GotoAsync("https://automationexercise.com/login");

            await _actor.AttemptsTo(
                LoginTask.WithDetails(email, password)
            );

            await ScreenshotHelper.CaptureScreenshot(Page, "login_screenplay.png");
        }

        [Test, Order(3)]
        [TestCase("Tshirt", "Men", "Polo")]
        [TestCase("Dress", "Women", "Biba")]
        public async Task Search_Products(string productName, string gender, string brandName)
        {
            await _actor.Page.GotoAsync("https://automationexercise.com/products");

            await _actor.AttemptsTo(
                SearchProductTask.For(productName, gender, brandName)
            );

            await ScreenshotHelper.CaptureScreenshot(Page, "search_screenplay.png");
        }

        [Test, Order(4)]
        [TestCase(1)]
        public async Task Cart_Quantity_Update(int quantityToAdd)
        {
            await _actor.Page.GotoAsync("https://automationexercise.com/products");

            await _actor.AttemptsTo(
                AddProductToCartTask.Add(quantityToAdd)
            );

            await ScreenshotHelper.CaptureScreenshot(Page, "cart_update_screenplay.png");
        }

        [Test, Order(5)]
        public async Task Remove_Item_From_Cart()
        {
            await _actor.Page.GotoAsync("https://automationexercise.com/view_cart");

            await _actor.AttemptsTo(
                RemoveItemFromCartTask.FirstItem()
            );

            await ScreenshotHelper.CaptureScreenshot(Page, "remove_cart_screenplay.png");
        }
    }
}