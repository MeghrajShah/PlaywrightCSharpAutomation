using Microsoft.Playwright;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;

namespace PlaywrightCSharpAssignment.utilities
{
    public static class ScreenshotHelper
    {
        public static async Task CaptureScreenshot(IPage page, string path)
        {
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = path });
        }

        public static void CompareScreenshots(string baselinePath, string currentPath)
        {
            byte[] baseline = File.ReadAllBytes(baselinePath);
            byte[] current = File.ReadAllBytes(currentPath);

            Assert.That(baseline, Is.EqualTo(current), "Screenshots differ! Possible visual regression.");
        }
    }
}