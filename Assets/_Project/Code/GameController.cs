#nullable enable
using System;
using NuclearBand;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } = null!;

    private GameWindow? _gameWindow;
    private MovementWindow? _movementWindow;
    
    private void Awake()
    {
        Instance = this;
        WindowsManager.Init(new WindowsManagerSettings
        {
            InputBlockPath = "GUI/InputBlocker",
            RootPath = "GUI/Canvas"
        });
        var backButtonEventManager = new GameObject().AddComponent<BackButtonEventManager>();
        backButtonEventManager.OnBackButtonPressed += WindowsManager.ProcessBackButton;

        MenuWindow.CreateWindow().Show();
    }

    public void StartGame()
    {
        _gameWindow = GameWindow.CreateWindow();
        _gameWindow.Show();
        _movementWindow = MovementWindow.CreateWindow();
        _movementWindow.Show();
    }

    public void FinishGame()
    {
        if (_gameWindow == null || _movementWindow == null)
            throw new NullReferenceException("if (_gameWindow == null || _movementWindow == null)");
        
        _gameWindow.Close();
        _movementWindow.Close();
        MenuWindow.CreateWindow().Show();
    }
}
