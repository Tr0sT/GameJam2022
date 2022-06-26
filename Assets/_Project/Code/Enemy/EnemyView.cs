#nullable enable
using System;
using System.Collections.Generic;
using NuclearBand;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyView : SerializedMonoBehaviour, IEnemy
{
    public Camera cam;
    public Sprite spriteDead;

    [NonSerialized] [OdinSerialize] private EnemySettings _enemySettings = null!;

    private int _health;
    [SerializeField] 
    private List<Sprite> _sprites = new();

    [SerializeField] 
    private SpriteRenderer _spriteRenderer = null!;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.GetComponent<PlayerMovement>().jump)
                return;

            collider.GetComponent<PlayerHealth>().TakeDamage(_enemySettings.Damage);
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
            Destroy(gameObject, 0.2f);
            AudioManager.PlaySound("237926__foolboymedia__messy-splat-2");
            cam.GetComponent<Animator>().Play("cameraAnim", 0, 0.25f);
        }
    }

    public event Action<IEnemy>? OnDestroy;


    public void Init(Vector3 position, IEnemySettings enemySettings)
    {
        _enemySettings = (EnemySettings) enemySettings;
        transform.localPosition = position.WithZ(0);
        _health = _enemySettings.Health;
        GetComponent<Movement>().Speed = _enemySettings.Speed;
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
    }


    public void DeInit()
    {
        OnDestroy = null;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = spriteDead;
            gameObject.GetComponentInChildren<Animator>().enabled = false;
            gameObject.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject, 2);
            //OnDestroy?.Invoke(this);
        }
    }
}