namespace AmpShell.AvaloniaUI
{
    using Avalonia;
    using Avalonia.ReactiveUI;
    using System.Diagnostics;
    using System.Linq;

    internal class Program
    {
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            if (args.Any())
            {
                Process.Start("AmpShell.Cli", string.Join(" ", args));
                return;
            }
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
    }
}