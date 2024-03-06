using Microsoft.Win32;
using System;
using System.Threading;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    static void Main(string[] args)
    {
        var handle = GetConsoleWindow();
        ShowWindow(handle, SW_HIDE);

        // To show the console window (if needed):
        // ShowWindow(handle, SW_SHOW);

        int refreshInterval = 50; // default value
        if (args.Length > 0 && args[0] == "--refresh" && args.Length > 1 && int.TryParse(args[1], out int argValue))
        {
            refreshInterval = argValue;
        }

        while (true)
        {
            CheckAndUpdateRegistryValue(@"Control Panel\NotifyIconSettings");
            Thread.Sleep(TimeSpan.FromSeconds(refreshInterval));
        }
    }

    static void CheckAndUpdateRegistryValue(string registryPath)
    {
        const string propertyName = "IsPromoted";

        using (var key = Registry.CurrentUser.OpenSubKey(registryPath, writable: true))
        {
            if (key != null)
            {
                var propertyValue = key.GetValue(propertyName);
                if (propertyValue == null)
                {
                    // If the key doesn't exist, create it and set its value to 1
                    key.SetValue(propertyName, 1, RegistryValueKind.DWord);
                }
                else if (propertyValue.ToString() == "0")
                {
                    // If the key exists and its value is 0, set its value to 1
                    key.SetValue(propertyName, 1, RegistryValueKind.DWord);
                }

                // Recursively check all subkeys
                foreach (string subkeyName in key.GetSubKeyNames())
                {
                    CheckAndUpdateRegistryValue(registryPath + "\\" + subkeyName);
                }
            }
        }
    }
}