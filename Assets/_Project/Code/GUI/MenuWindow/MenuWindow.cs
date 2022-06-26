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

    public override void Init()
    {
        base.Init();
        AudioManager.PlayMusic("CryinInMyBeer", true);
    }

    public void OnPlayClick()
    {
        Close();
        AudioManager.PlaySound("Нажатиекнопки");
        GameController.Instance.StartGame();
    }

    public void OnHelpClick()
    {
        SettingsPopup.CreateWindow().Show();
    }

    public override void OnBackButtonPressedCallback()
    {
        Application.Quit();
    }
}
