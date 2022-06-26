#nullable enable
using System;
using System.Collections.Generic;
using NuclearBand;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } = null!;

    [SerializeField]
    private EnemySpawnController _enemySpawnController = null!;
    
    [SerializeField]
    private BulletSpawnController _bulletSpawnController = null!;
    

    [SerializeField] 
    private ShootController _shootController = null!;

    [SerializeField] 
    private GameObject _game = null!;

    private GameWindow? _gameWindow;

    private List<IEnemy> _enemies = new();

    private bool _active;
    
    private void Awake()
    {
        Instance = this;
        
        _enemySpawnController.Init();
        _bulletSpawnController.Init();
        _game.SetActive(false);
        
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
        _gameWindow = GameWindow.CreateWindow(_shootController.Shoot);
        _gameWindow.Show();
        
        _shootController.Init(_gameWindow.ShootJoystick);

        for (var i = 0; i < 1; i++)
        {
            _enemies.Add(_enemySpawnController.SpawnRandom());
        }
        
        _game.SetActive(true);
        _active = true;
    }


    private float _spawnTime = 2.0f;
    private float _curTime = 0.0f;
    private void Update()
    {
        if (!_active)
            return;

        if (_curTime >= _spawnTime)
        {
            _enemies.Add(_enemySpawnController.SpawnRandom());

            _curTime = 0;
        }

        _curTime += Time.deltaTime;
    }

    public void FinishGame()
    {
        if (_gameWindow == null)
            throw new NullReferenceException("if (_gameWindow == null || _movementWindow == null)");
        
        _game.SetActive(false);
        _gameWindow.Close();
        MenuWindow.CreateWindow().Show();
        _active = false;
    }
}
