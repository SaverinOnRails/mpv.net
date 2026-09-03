using Avalonia;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MpvNet.Avalonia;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    private static void StartAvaloniaApp(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    [STAThread]
    public static void Main(string[] args)
    {
        StartAvaloniaApp(args);
    }

    private static nint DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName == "mpv")
        {
            if (OperatingSystem.IsWindows())
            {
                return NativeLibrary.Load("libmpv-2.dll");

            }
            else if (OperatingSystem.IsLinux())
            {
                return NativeLibrary.Load("mpv");
            }
            else return nint.Zero;
        }

        // Otherwise, fallback to default import resolver.
        return nint.Zero;
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
