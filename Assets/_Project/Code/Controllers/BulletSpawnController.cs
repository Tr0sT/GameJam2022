#nullable enable
using System;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawnController : MonoBehaviour
{
    public static BulletSpawnController Instance { get; private set; } = null!;

    [SerializeField]
    private SawBulletView _sawBulletPrefab = null!;

    [SerializeField] 
    private Transform _bulletsPoolParent = null!;
    
    [SerializeField] 
    private Transform _bulletsParent = null!;
    
    private IObjectPool<IBullet> _bulletPool = null!;

    public void Init()
    {
        Instance = this;
        
        _sawBulletPrefab.gameObject.SetActive(false);
        
        _bulletPool = new ObjectPool<IBullet>(() => 
                GameObject.Instantiate(_sawBulletPrefab.gameObject, _bulletsPoolParent).GetComponent<IBullet>(), 
            view =>
            {
                (view as MonoBehaviour)!.gameObject.SetActive(true);
                (view as MonoBehaviour)!.transform.parent = _bulletsParent;
            }, view =>
            {
                view.DeInit();
                (view as MonoBehaviour)!.transform.parent = _bulletsPoolParent;
                (view as MonoBehaviour)!.gameObject.SetActive(false);
                
            }, view =>
            {
                GameObject.Destroy((view as MonoBehaviour)!.gameObject);
            }, true, 20);
    }

    public void SpawnBullet(Vector3 position, Vector3 direction, IBulletSettings bulletSettings)
    {
        var bullet = _bulletPool.Get();
        bulletSettings.StartPosition = position;
        bulletSettings.StartDirection = direction;
        
        bullet.Init(bulletSettings);
        bullet.OnDestroy += OnDestroyBulletCallback;
    }

    private void OnDestroyBulletCallback(IBullet bullet)
    {
        try
        {
            _bulletPool.Release(bullet);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
