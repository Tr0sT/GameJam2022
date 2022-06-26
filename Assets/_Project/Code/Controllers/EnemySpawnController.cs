#nullable enable
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyPrefab = null!;

    [SerializeField] private Transform _enemiesPoolParent = null!;

    [SerializeField] private Transform _enemiesParent = null!;

    private IObjectPool<IEnemy> _enemyPool = null!;
    public static EnemySpawnController Instance { get; private set; } = null!;

    public void Init()
    {
        Instance = this;

        _enemyPrefab.gameObject.SetActive(false);

        _enemyPool = new ObjectPool<IEnemy>(() =>
                Instantiate(_enemyPrefab.gameObject, _enemiesPoolParent).GetComponent<IEnemy>(),
            view =>
            {
                (view as MonoBehaviour)!.gameObject.SetActive(true);
                (view as MonoBehaviour)!.transform.parent = _enemiesParent;
            }, view =>
            {
                view.DeInit();
                (view as MonoBehaviour)!.transform.parent = _enemiesPoolParent;
                (view as MonoBehaviour)!.gameObject.SetActive(false);
            }, view => { Destroy((view as MonoBehaviour)!.gameObject); }, true, 20);
    }

    public IEnemy SpawnEnemy(Vector2 position, IEnemySettings enemySettings)
    {
        var enemy = _enemyPool.Get();
        enemy.Init(position, enemySettings);
        enemy.OnDestroy += OnDestroyBullet;
        return enemy;
    }

    private void OnDestroyBullet(IEnemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    public IEnemy SpawnRandom(IEnemySettings enemySettings)
    {
        var side = Random.Range(0, 2000 * 2 + 1000 * 2);
        Vector2 position = Vector2.zero;
        if (side <= 2000) // top
            position = new Vector2(Random.Range(-1000, 1000), 500);
        else if (side <= 3000)  // right
            position = new Vector2(Random.Range(-700, 700), -500);
        else if (side <= 5000) // bottom
            position = new Vector2(Random.Range(-1000, 1000), -500);
        else  // left
            position = new Vector2(Random.Range(-700, 700), 500);
        
        return SpawnEnemy(position, enemySettings);
    }
}