#nullable enable

using NuclearBand;
using Unity.VisualScripting;
using UnityEngine;

public class WinLoseWindow : FridgeWindow
{
    #region Creation
    private const string Path = "GUI/WinLoseWindow/WinLoseWindow";

    public static WinLoseWindow CreateWindow(bool win)
    {
        return (WinLoseWindow)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (WinLoseWindow)window;
            w._win = win;
        }).Window;
    }
    #endregion


    [SerializeField] 
    private GameObject _winGO = null!, _loseGO = null!;
    private bool _win;

    public override void Init()
    {
        base.Init();
        _winGO.SetActive(_win);
        _loseGO.SetActive(!_win);
        GameController.Instance.Pause(true);
    }

    public override void Close()
    {
        base.Close();
        GameController.Instance.Pause(false);
        GameController.Instance.FinishGame();
        GameController.Instance.StartGame();
    }
}
