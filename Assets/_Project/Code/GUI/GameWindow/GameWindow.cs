#nullable enable
using System;
using NuclearBand;

public class GameWindow : FridgeWindow
{
    #region Creation
    private const string Path = "GUI/GameWindow/GameWindow";

    public static GameWindow CreateWindow(Action shootAction)
    {
        return (GameWindow)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (GameWindow)window;
            w._shootAction = shootAction;
        }).Window;
    }
    #endregion


    public Joystick MovementJoystick = null!;
    public Joystick ShootJoystick = null!;

    private Action? _shootAction;

    public void OnShoot()
    {
        _shootAction?.Invoke();
    }
    
    public override void OnBackButtonPressedCallback()
    {
        GameController.Instance.FinishGame();
    }
}
