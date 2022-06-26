#nullable enable
using System;
using System.Collections.Generic;
using NuclearBand;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWindow : FridgeWindow
{
    public static GameWindow Instance { get; private set; } = null!;

    #region Creation

    private const string Path = "GUI/GameWindow/GameWindow";

    public static GameWindow CreateWindow(Action shootAction)
    {
        return (GameWindow) WindowsManager.CreateWindow(Path, window =>
        {
            var w = (GameWindow) window;
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

    [SerializeField] 
    private Image _jumpButton = null!;

    public override void Init()
    {
        base.Init();
        SetHP(1.0f);
        AudioManager.PlayMusic("Pentagram", true);
        ShootJoystick.OnShoot -= OnShoot;
        ShootJoystick.OnShoot += OnShoot;
    }

    public void OnShoot()
    {
        _shootAction?.Invoke();
    }

    public void OnJump()
    {
        PlayerMovement.Instance.Jump();
    }

    private void Update()
    {
        _jumpButton.gameObject.SetActive(ShootController.Instance.CanJump());
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
    }

    public void Restart()
    {
        Close();
        SceneManager.LoadScene(0);
    }
}