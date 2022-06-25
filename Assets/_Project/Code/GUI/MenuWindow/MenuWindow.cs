#nullable enable
using NuclearBand;
using UnityEngine;

public class MenuWindow : FridgeWindow
{
    #region Creation
    private const string Path = "GUI/MenuWindow/MenuWindow";

    public static MenuWindow CreateWindow()
    {
        return (MenuWindow)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (MenuWindow)window;
        }).Window;
    }
    #endregion

    public void OnPlayClick()
    {
        Close();
        GameController.Instance.StartGame();
    }

    public void OnHelpClick()
    {
        //HelpWindow.CreateWindow().Show();
    }

    public void OnSettingsClick()
    {
        SettingsPopup.CreateWindow().Show();
    }

    public void OnCustomizationClick()
    {
        CustomizationWindow.CreateWindow().Show();
    }

    public override void OnBackButtonPressedCallback()
    {
        Application.Quit();
    }
}
