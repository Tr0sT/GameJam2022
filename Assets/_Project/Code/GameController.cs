#nullable enable
using System;
using System.Collections.Generic;
using NuclearBand;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : SerializedMonoBehaviour
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

        _game.SetActive(true);
        _active = true;
    }


    private float _curSpawnTime = 0.0f;

    private int _curStage = 0;
    private float _curStageTime;
    [OdinSerialize]
    private List<Stage> _stages = new();
    private void Update()
    {
        if (!_active)
            return;

        if (_curStage >= _stages.Count)
            return;
        if (_curStageTime > _stages[_curStage].Time)
        {
            _curStage++;
            _curStageTime = 0;
            
            if (_curStage >= _stages.Count)
                return;
        }
        if (_curSpawnTime >= _stages[_curStage].SpawnDelay && _enemies.Count < _stages[_curStage].MaxEnemiesCount)
        {
            _enemies.Add(_enemySpawnController.SpawnRandom(_stages[_curStage].Enemies[0]));

            _curSpawnTime = 0;
        }

        _curSpawnTime += Time.deltaTime;
        _curStageTime += Time.deltaTime;
    }

    public void FinishGame()
    {
        if (_gameWindow == null)
            throw new NullReferenceException("if (_gameWindow == null || _movementWindow == null)");
        
        _game.SetActive(false);
        _gameWindow.Close();
        SceneManager.LoadScene(0);
        //MenuWindow.CreateWindow().Show();
        _active = false;
    }
}
