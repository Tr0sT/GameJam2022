#nullable enable
using System;
using System.Collections.Generic;
using NuclearBand;
using UnityEngine;
using UnityEngine.UI;

public class GameWindow : FridgeWindow
{
    public static GameWindow Instance { get; private set; } = null!;
    #region Creation
    private const string Path = "GUI/GameWindow/GameWindow";

    public static GameWindow CreateWindow(Action shootAction)
    {
        return (GameWindow)WindowsManager.CreateWindow(Path, window =>
        {
            var w = (GameWindow)window;
            w._shootAction = shootAction;
            Instance = w;
        }).Window;
    }
    #endregion


    public Joystick MovementJoystick = null!;
    public Joystick ShootJoystick = null!;

    private Action? _shootAction;
    
    [SerializeField] 
    private List<Image> _hpImages = new();

    public override void Init()
    {
        base.Init();
        SetHP(1.0f);
        AudioManager.PlayMusic("Pentagram", true);
    }

    public void OnShoot()
    {
        _shootAction?.Invoke();
    }

    public void SetHP(float proc)
    {
        foreach (var hpImage in _hpImages)
        {
            hpImage.color = new Color(hpImage.color.r, hpImage.color.g, hpImage.color.b, 1 - proc);
        }
    }
    
    public override void OnBackButtonPressedCallback()
    {
        GameController.Instance.FinishGame();
    }
}
