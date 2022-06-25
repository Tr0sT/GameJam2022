#nullable enable

using NuclearBand;

public class CustomizationWindow : FridgeWindow
{
    #region Creation
    private const string Path = "GUI/CustomizationWindow/CustomizationWindow";

    public static CustomizationWindow CreateWindow()
    {
        return (CustomizationWindow)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (CustomizationWindow)window;
        }).Window;
    }
    #endregion

}
