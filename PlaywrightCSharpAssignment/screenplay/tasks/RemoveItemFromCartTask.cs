using System.Threading.Tasks;
using PlaywrightCSharpAssignment.screenplay;
using Microsoft.Playwright;

namespace PlaywrightCSharpAssignment.screenplay.tasks
{
    public class RemoveItemFromCartTask : ITask
    {
        private RemoveItemFromCartTask() {}

        public static RemoveItemFromCartTask FirstItem()
        {
            return new RemoveItemFromCartTask();
        }

        public async Task PerformAs(Actor actor)
        {
            var page = actor.Page;

            await page.Locator(".cart_quantity_delete").First.ClickAsync();
            var emptyCartText = page.Locator("b:has-text(\"Cart is empty!\")");

            await emptyCartText.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000
            });
        }
    }
}