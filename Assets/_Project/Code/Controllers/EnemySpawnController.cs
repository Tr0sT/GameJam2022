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
    
    private IObjectPool<EnemyView> _enemyPool = null!;

    public void Init()
    {
        Instance = this;
        
        _enemyPrefab.gameObject.SetActive(false);
        
        _enemyPool = new ObjectPool<EnemyView>(() => 
                GameObject.Instantiate(_enemyPrefab.gameObject, _enemiesPoolParent).GetComponent<EnemyView>(), 
            view =>
            {
                view.gameObject.SetActive(true);
                view.transform.parent = _enemiesParent;
            }, view =>
            {
                view.DeInit();
                view.transform.parent = _enemiesPoolParent;
            }, view =>
            {
                GameObject.Destroy(view.gameObject);
            }, true, 20);
    }

    public void SpawnEnemy(Vector2 position, object enemySettings)
    {
        var enemy = _enemyPool.Get();
        enemy.Init(position, enemySettings);
    }
}
