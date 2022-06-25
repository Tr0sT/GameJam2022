#nullable enable

using NuclearBand;

public class SettingsPopup : FridgeWindow
{
    #region Creation
    private const string Path = "GUI/SettingsPopup/SettingsPopup";

    public static SettingsPopup CreateWindow()
    {
        return (SettingsPopup)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (SettingsPopup)window;
        }).Window;
    }
    #endregion
}
