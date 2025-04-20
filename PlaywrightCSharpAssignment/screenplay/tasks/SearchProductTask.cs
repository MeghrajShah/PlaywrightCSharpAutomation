using System.Threading.Tasks;
using PlaywrightCSharpAssignment.screenplay;
using Microsoft.Playwright;

namespace PlaywrightCSharpAssignment.screenplay.tasks
{
    public class SearchProductTask : ITask
    {
        private readonly string _productName;
        private readonly string _gender;
        private readonly string _brandName;

        private SearchProductTask(string productName, string gender, string brandName)
        {
            _productName = productName;
            _gender = gender;
            _brandName = brandName;
        }

        public static SearchProductTask For(string productName, string gender, string brandName)
        {
            return new SearchProductTask(productName, gender, brandName);
        }

        public async Task PerformAs(Actor actor)
        {
            var page = actor.Page;

            await page.Locator("#search_product").FillAsync(_productName);
            await page.Locator("#submit_search").ClickAsync();

            // Filtering
            await page.Locator($"a:has-text(\"{_brandName}\")").ClickAsync();
            await page.Locator($"a[href=\"#{_gender}\"]").ClickAsync();
        }
    
    }
}