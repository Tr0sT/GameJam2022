#nullable enable
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawnController : MonoBehaviour
{
    public static EnemySpawnController Instance { get; private set; } = null!;

    [SerializeField]
    private EnemyView _enemyPrefab = null!;

    [SerializeField] 
    private Transform _enemiesPoolParent = null!;
    
    [SerializeField] 
    private Transform _enemiesParent = null!;
    
    private IObjectPool<IEnemy> _enemyPool = null!;

    public void Init()
    {
        Instance = this;
        
        _enemyPrefab.gameObject.SetActive(false);
        
        _enemyPool = new ObjectPool<IEnemy>(() => 
                GameObject.Instantiate(_enemyPrefab.gameObject, _enemiesPoolParent).GetComponent<IEnemy>(), 
            view =>
            {
                (view as MonoBehaviour)!.gameObject.SetActive(true);
                (view as MonoBehaviour)!.transform.parent = _enemiesParent;
            }, view =>
            {
                view.DeInit();
                (view as MonoBehaviour)!.transform.parent = _enemiesPoolParent;
            }, view =>
            {
                GameObject.Destroy((view as MonoBehaviour)!.gameObject);
            }, true, 20);
    }

    public IEnemy SpawnEnemy(Vector2 position, IEnemySettings enemySettings)
    {
        var enemy = _enemyPool.Get();
        enemy.Init(position, enemySettings);
        return enemy;
    }

    public void DestroyEnemy(IEnemy enemy)
    {
        _enemyPool.Release(enemy);
    }
}
