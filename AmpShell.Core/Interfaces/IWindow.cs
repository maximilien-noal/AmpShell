namespace AmpShell.Core.Interfaces
{
    /// <summary>
    /// It has been reported that on some systems, when the game goes fullscreen the MainWindow size
    /// is affected by the change of resolution. So we need to disappear before the game is running,
    /// and reappear after the game exited.
    /// </summary>
    public interface IWindow
    {
        void Minimize();

        void Restore();
    }
}