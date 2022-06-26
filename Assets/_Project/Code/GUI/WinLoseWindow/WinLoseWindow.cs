#nullable enable

using NuclearBand;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (_win)
            AudioManager.PlaySound("Победа");
        else
        {
            AudioManager.PlaySound("Поражение");
        }
            
        GameController.Instance.Pause(true);
    }

    public override void Close()
    {
        base.Close();
        GameWindow.Instance.Close();
        GameController.Instance.Pause(false);
        SceneManager.LoadScene(0);
    }
}
