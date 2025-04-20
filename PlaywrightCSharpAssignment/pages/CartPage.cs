using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightCSharpAssignment.Pages
{
    public class CartPage
    {
        private readonly IPage _page;
        public CartPage(IPage page)
        {
            _page = page;
        }

        public async Task<int> GetCartQuantity()
        {
            var cartLink = _page.Locator("a[href='/view_cart']:has-text(\"Cart\")");
            await cartLink.Nth(0).IsVisibleAsync();
            await cartLink.Nth(0).ClickAsync();

            var productRow = _page.Locator("tr[id^='product-']");
            //var quantityText = await _page.Locator(".cart_quantity_input").InputValueAsync();
            return await productRow.CountAsync();
        }

        public async Task RemoveFirstItem()
        {
            await _page.Locator(".cart_quantity_delete").First.ClickAsync();
        }

    }
}