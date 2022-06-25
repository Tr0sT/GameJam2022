#nullable enable
using NuclearBand;

public class GameWindow : FridgeWindow
{
    #region Creation
    private const string Path = "GUI/GameWindow/GameWindow";

    public static GameWindow CreateWindow()
    {
        return (GameWindow)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (GameWindow)window;
        }).Window;
    }
    #endregion
    
    public override void OnBackButtonPressedCallback()
    {
        GameController.Instance.FinishGame();
    }
}
