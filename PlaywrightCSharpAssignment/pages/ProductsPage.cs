using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightCSharpAssignment.Pages
{
    public class ProductsPage
    {
        private readonly IPage _page;
        private readonly ILocator _searchBox;
        private readonly ILocator _searchButton;
        private readonly ILocator _womensLink;
        private readonly ILocator _brandLink;

        public ProductsPage(IPage page)
        {
            _page = page;
            _searchBox = page.Locator("#search_product");
            _searchButton = page.Locator("#submit_search");
            
        }
            

        public async Task SearchProduct(string productName)
        {
            await _searchBox.FillAsync(productName);
            await _searchButton.ClickAsync();
        }

        public async Task filterByBrand(string brandName){
            var brandLink = _page.Locator($"a[href='/brand_products/{brandName}']");

            await brandLink.IsVisibleAsync();
            await brandLink.ClickAsync();
        }

         public async Task filterByProductType(string gender, string categoryName){
            
            var genderLink = _page.Locator($"a[href='#{gender}']");

            await genderLink.IsVisibleAsync();
            await genderLink.ClickAsync();

            var categoryLink = _page.Locator($"a:has-text(\"{categoryName}\")");

            if(gender.ToLower().Equals("women")){
                categoryLink = _page.Locator($"a[href='/category_products/1']:has-text(\"{categoryName}\")");
            }

            await categoryLink.IsVisibleAsync();
            await categoryLink.ClickAsync();
         }

        private async Task HandleConsentDialog()
        {
            var consentButton = _page.Locator("div.fc-dialog button:has-text(\"Consent\")");

            try
            {
                // Explicitly wait for the button to be visible and enabled
            await consentButton.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
            });

            await consentButton.ClickAsync();
                
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