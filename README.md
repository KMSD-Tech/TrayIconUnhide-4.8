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

## Conclusion

After completing these steps, TrayIconUnhide will run automatically at startup and refresh your tray icons at the specified interval.
