using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace TrayIconUnhide
{
    public partial class Form1 : Form
    {
        private int refreshInterval = 50; // default value
        private bool runOnce = false;

        public Form1(string[] args)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--refresh" && i + 1 < args.Length && int.TryParse(args[i + 1], out int argValue))
                {
                    refreshInterval = argValue;
                }
                else if (args[i] == "--runonce")
                {
                    runOnce = true;
                }
            }

            CheckAndUpdateRegistryValue(@"Control Panel\NotifyIconSettings");

            if (runOnce)
            {
                Environment.Exit(0);
            }

            if (!runOnce)
            {
                System.Timers.Timer timer = new System.Timers.Timer(refreshInterval * 1000); // convert to milliseconds
                timer.Elapsed += (sender, e) => CheckAndUpdateRegistryValue(@"Control Panel\NotifyIconSettings");
                timer.Start();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(this.DesignMode); // Only show in design mode
        }

        private void CheckAndUpdateRegistryValue(string registryPath)
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
}