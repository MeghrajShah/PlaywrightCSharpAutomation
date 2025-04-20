using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightCSharpAssignment.tests
{
    [TestFixture]
    [Category("API")]
    public class ApiTests
    {
        private IAPIRequestContext _apiContext;

        [SetUp]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _apiContext = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                ExtraHTTPHeaders = new Dictionary<string, string>
                {
                    { "Accept", "application/json" }
                }
            });
        }

        [TearDown]
        public async Task TearDown()
        {
            await _apiContext.DisposeAsync();
        }

        [Test]
        [Order(1)]
        public async Task GetProducts_ShouldReturnStatusOk_AndExpectedFields()
        {
            var response = await _apiContext.GetAsync("https://automationexercise.com/products");

            // Assert that status code is 200 OK
            Assert.That(response.Status, Is.EqualTo(200), "Expected status code 200 OK");

            // Get response body as text
            var body = await response.TextAsync();

            // Check body contains some expected fields
            Assert.That(body, Does.Contain("All Products"));
            Assert.That(body, Does.Contain("Brands"));
        }


        [Test]
        [TestCase("Tshirt")]
        [TestCase("top")]
        [TestCase("jean")]
        public async Task SearchProduct_ShouldReturnStatusOk_AndExpectedProducts(string searchTerm)
        {
            // Send post request and check response has expected products
            var formData = $"search_product={System.Net.WebUtility.UrlEncode(searchTerm)}";

            var response = await _apiContext.PostAsync(
                "https://automationexercise.com/api/searchProduct",
                new APIRequestContextOptions
                {
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/x-www-form-urlencoded" }
                    },
                    Data = formData 
            });

            Assert.That(response.Status, Is.EqualTo(200), $"Expected 200 OK for search '{searchTerm}'");

            var jsonBody = await response.JsonAsync();

            TestContext.WriteLine($"Response for search '{searchTerm}': {jsonBody}");

            var productsArray = jsonBody?.GetProperty("products");

            Assert.That(productsArray.HasValue, Is.True, "Expected 'products' field in response");
            Assert.That(productsArray?.ValueKind.ToString(), Is.EqualTo("Array"), "Expected 'products' to be an array");
            Assert.That(productsArray?.GetArrayLength(), Is.GreaterThan(0), "Expected at least one product in search results");
        }
    }
}