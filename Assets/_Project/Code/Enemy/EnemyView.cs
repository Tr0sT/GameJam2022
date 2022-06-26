#nullable enable
using System;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class EnemyView : SerializedMonoBehaviour, IEnemy
{
    public event Action<IEnemy>? OnDestroy;
    public Camera cam;
    public Sprite spriteDead;
    [NonSerialized]
    [OdinSerialize]
    private EnemySettings _enemySettings = null!;

    private int _health;


    public void Init(Vector3 position, IEnemySettings enemySettings)
    {
        _enemySettings = (EnemySettings)enemySettings;
        transform.localPosition = position.WithZ(0);
        _health = _enemySettings.Health;
        GetComponent<Movement>().Speed = _enemySettings.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth>().TakeDamage(_enemySettings.Damage);
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
                Destroy(gameObject,0.2f);
                cam.GetComponent<Animator>().Play("cameraAnim", 0, 0.25f);
                return;
            }
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
            gameObject.GetComponent<Transform>().localScale =  new Vector3(0.5f,0.5f, 0.5f);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject, 2);
            //OnDestroy?.Invoke(this);
        }
    }
}