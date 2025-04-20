using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightCSharpAssignment.Pages;
using PlaywrightCSharpAssignment.utilities;
using System.Diagnostics;

namespace PlaywrightCSharpAssignment.tests
{
    [TestFixture]
    [Category("UI")]
    public class UITests : BaseTest
    {
        private static readonly Random _random = new Random();
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string Generate(int length)
    {
        return new string(Enumerable.Repeat(_chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
        string email = Generate(8)+"@mailinator.com";   
        string password = "password123";
        string productSubType = "";

        [Test, Order(1)]
        public async Task Signup_NewUser()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await Page.GotoAsync("https://automationexercise.com/login");
            await Page.Locator("button:has-text(\"Consent\")").ClickAsync();

            stopwatch.Stop();
            var loadTimeInMilliseconds = stopwatch.ElapsedMilliseconds;

            TestContext.WriteLine($"Page Load Time: {loadTimeInMilliseconds} ms");


            var loginPage = new LoginPage(Page);
            await loginPage.Signup("TestUser", email, password);
            await ScreenshotHelper.CaptureScreenshot(Page, "signup.png");
        }

        [Test, Order(2)]
        public async Task Login_Test()
        {
            await Page.GotoAsync("https://automationexercise.com/login");
            var loginPage = new LoginPage(Page);
            await loginPage.Login(email, password);
            await ScreenshotHelper.CaptureScreenshot(Page, "login.png");
        }

        [Order(3)]
        [TestCase("Tshirt", "Men", "Polo")]
        [TestCase("Dress", "Women", "Biba")]
        public async Task Search_Products(string productName, string gender, string brandName)
        {
            await Page.GotoAsync("https://automationexercise.com/products");
            var productsPage = new ProductsPage(Page);
            
            if(productName.ToLower().Equals("tshirt") && gender.ToLower().Equals("men")) {productSubType = "Tshirts";}
            if(productName.ToLower().Equals("dress") && gender.ToLower().Equals("women")) {productSubType = "Dress";}
            await productsPage.SearchProduct(productName);

            // Filter products by brand

            await productsPage.filterByBrand(brandName);

            //check the page title to check that the relevent filter is active
            var titleTextLocator = Page.Locator($"h2:has-text(\"Brand - {brandName.ToUpper()}\")");
            Assert.That(await titleTextLocator.IsVisibleAsync(), Is.True);


            //filter products by gender and product sub-type
            await productsPage.filterByProductType(gender,productSubType);
            
            //check the page title to check that the relevent filter is active
            titleTextLocator = Page.Locator($"h2:has-text(\"{gender} - {productName.ToUpper()}\")");
            Assert.That(await titleTextLocator.IsVisibleAsync(), Is.True);

        }

        [Order(4)]
        [TestCase(1)]
        public async Task Cart_Quantity_Update(int quantityToAdd)
        {
            await Page.GotoAsync("https://automationexercise.com/products");

                        // Get all "Add to cart" buttons
            var addToCartButtons = Page.Locator("a:has-text(\"Add to cart\")");

            // Wait for at least one to appear
            await addToCartButtons.First.WaitForAsync();

            // Get how many "Add to cart" buttons found
            int count = await addToCartButtons.CountAsync();

            Assert.That(count, Is.GreaterThanOrEqualTo(1));
            Console.WriteLine($"Total Add to cart buttons found: {count}");


            // Loop and click each
            for (int i = 0; i < quantityToAdd; i++)
            {
                await addToCartButtons.Nth(i).ClickAsync();
                await Page.Locator("button:has-text(\"Continue Shopping\")").IsVisibleAsync();

                // Assert the number of items in cart is as expected
                Assert.That(Page.Locator("button:has-text(\"Continue Shopping\")").CountAsync, Is.GreaterThanOrEqualTo(1));
                await Page.Locator("button:has-text(\"Continue Shopping\")").ClickAsync();
            }

            var cartPage = new CartPage(Page);
            int quantity = await cartPage.GetCartQuantity();

            //Assert the cart has atleast 'quantityToAdd' items
            Assert.That(quantity, Is.GreaterThanOrEqualTo(quantityToAdd));

            await ScreenshotHelper.CaptureScreenshot(Page, "cart.png");
        }


        [Test]
        [Order(5)]
        public async Task Remove_Item_From_Cart()
        {
            await Page.GotoAsync("https://automationexercise.com/view_cart");
            var cartPage = new CartPage(Page);
            await cartPage.RemoveFirstItem();

            var emptyCartText = Page.Locator("b:has-text(\"Cart is empty!\")");

            await emptyCartText.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000 // wait up to max 5 seconds -  because there is a clear delay between cart emptying and 'cart is empty' text being shown
            });

            // Assert the cart is empty
            Assert.That( await emptyCartText.IsVisibleAsync(), Is.True);
        }

    }
}