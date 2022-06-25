#nullable enable

using NuclearBand;

public class MovementWindow : FridgeWindow
{
    #region Creation
    private const string Path = "GUI/MovementWindow/MovementWindow";

    public static MovementWindow CreateWindow()
    {
        return (MovementWindow)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (MovementWindow)window;
        }).Window;
    }
    #endregion

}
