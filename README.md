7/31/2024 - I added some new code to help with the Form not always hiding on some platforms.  As I had tested this on more systems, I found that on some devices, it would show a top bar for the Form1 application.
Adding this is supposed to repair that problem:
```
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(this.DesignMode); // Only show in design mode
        }
```
4/26/2024 - If you had previously downloaded this application - I just pushed a brand new release.  The original application was written as a console application.  I found that on some systems the console window would not reliably hide.  I rewrote this new version as a Forms application instead and tested each of the core features.  It now seems to reliably hide on the few systems where it was not doing that before.

# TrayIconUnhide Setup Instructions

Follow these steps to set up TrayIconUnhide on your computer. This tool is designed to periodically unhide tray icons on Windows.

## Step 1: Choose an Executable Location

First, decide on a location on your computer to store the executable file. It's recommended to choose a location that is accessible to all users. For example:

```
C:\ProgramData\Scripts\TrayIconUnhide.exe
```

## Step 2: Update Windows Registry

1. Open the Registry Editor by typing `regedit` in the Windows search bar and pressing Enter.
2. Navigate to the following path:

```
HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run
```

3. Create a new string value (REG_SZ) with the name `TrayIconUnhide`.
4. Set the value to the path of where you stored the script. For example:

```
C:\ProgramData\Scripts\TrayIconUnhide.exe
```

## Step 3: Configure Refresh Interval

The default interval for unhiding tray icons is set to 50 seconds. You can customize this interval to your preference using the following command format:

```
C:\ProgramData\Scripts\TrayIconUnhide.exe --refresh <IntervalInSeconds>
```

Replace `<IntervalInSeconds>` with your desired interval. For example, to set the interval to 10 seconds:

```
C:\ProgramData\Scripts\TrayIconUnhide.exe --refresh 10
```

## Optional - Run the program once and then close

```
C:\ProgramData\Scripts\TrayIconUnhide.exe --runonce
```

Run in this mode, it will unhide the icons once and then immediately close - this is useful if you don't want the program to run continously

## Conclusion

After completing these steps, TrayIconUnhide will run automatically at startup and refresh your tray icons at the specified interval.
