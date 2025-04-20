using System.Diagnostics;

namespace PlaywrightCSharpAssignment.utilities
{
    public static class PlaywrightInstaller
    {
        public static void InstallBrowsers()
        {
            var process = new Process();
            process.StartInfo.FileName = "pwsh"; // use "bash" if no pwsh installed
            process.StartInfo.Arguments = "bin/Debug/net8.0/playwright.ps1 install";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            System.Console.WriteLine(output);

            if (process.ExitCode != 0)
            {
                System.Console.WriteLine($"Error installing browsers: {error}");
            }
        }
    }
}