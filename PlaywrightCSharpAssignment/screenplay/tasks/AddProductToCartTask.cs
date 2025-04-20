using System.Threading.Tasks;
using PlaywrightCSharpAssignment.screenplay;
using Microsoft.Playwright;

namespace PlaywrightCSharpAssignment.screenplay.tasks
{
    public class AddProductToCartTask : ITask
    {
        private readonly int _quantity;

        private AddProductToCartTask(int quantity)
        {
            _quantity = quantity;
        }

        public static AddProductToCartTask Add(int quantity)
        {
            return new AddProductToCartTask(quantity);
        }

        public async Task PerformAs(Actor actor)
        {
            var page = actor.Page;

            var addToCartButtons = page.Locator("a:has-text(\"Add to cart\")");
            await addToCartButtons.First.WaitForAsync();

            for (int i = 0; i < _quantity; i++)
            {
                await addToCartButtons.Nth(i).ClickAsync();
                await page.Locator("button:has-text(\"Continue Shopping\")").ClickAsync();
            }
        }
    }
}