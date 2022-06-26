#nullable enable
using System;
using NuclearBand;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class SawBulletView : SerializedMonoBehaviour, IBullet
{
    public event Action<IBullet>? OnDestroy;
    public Camera cam;
    public int speedRotate = 300;
    [NonSerialized, OdinSerialize]
    private SawBulletSettings _sawBulletSettings = null!;

    public bool Active;

    public void Init(Vector3 position, Vector3 direction, IBulletSettings bulletSettings)
    {
        _sawBulletSettings = (SawBulletSettings)bulletSettings;
        transform.localPosition = position.WithZ(0);
        GetComponent<Movement>().Speed = _sawBulletSettings.speed;
        GetComponent<Movement>().Direction = direction;
        Active = true;
        Physics2D.IgnoreCollision(PlayerMovement.Instance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //Debug.Log("true");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.collider.CompareTag("Wall"))
            {
                cam.GetComponent<Animator>().Play("cameraAnim", 0, 0.25f);
            }
            if (contact.collider.CompareTag("Player"))
            {
                if (Active)
                {
                    contact.collider.GetComponent<PlayerHealth>().TakeDamage(_sawBulletSettings.Damage);
                    AudioManager.PlaySound("Звукпопадания");
                }
                else
                {
                    AudioManager.PlaySound("Подъемпилы");
                }
                ShootController.Instance.PickupSaw();
                DestroyBullet();
                return;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") && Active)
        {
            collider.GetComponent<IEnemy>().TakeDamage(_sawBulletSettings.Damage);
            AudioManager.PlaySound("Звукпопадания");
            cam.GetComponent<Animator>().Play("cameraAnim", 0, 0.25f);

            return;
        }
       

        //foreach (var contact in collision.contacts)
        //{
        //    if (contact.collider.CompareTag("Enemy") && Active)
        //    {
        //        contact.collider.GetComponent<IEnemy>().TakeDamage(_sawBulletSettings.Damage);
        //        //contact.collider.GetComponent<CircleCollider2D>().isTrigger = true;
        //        //DestroyBullet();
        //        return;
        //    }

        //    if (contact.collider.CompareTag("Player"))
        //    {
        //        if (Active)
        //        {
        //            contact.collider.GetComponent<PlayerHealth>().TakeDamage(_sawBulletSettings.Damage);
        //        }
        //        ShootController.Instance.PickupSaw();
        //        DestroyBullet();
        //        return;
        //    }
        //}
    }


    private void DestroyBullet()
    {
        OnDestroy?.Invoke(this);
    }

    public void DeInit()
    {
        _sawBulletSettings = null!;
        Active = false;
        OnDestroy = null;
        Physics2D.IgnoreCollision(PlayerMovement.Instance.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        Debug.Log("false");
    }
    private void FixedUpdate()
    {
        if (Active)
        {
            gameObject.transform.Rotate(0, 0, speedRotate * 1f * Time.deltaTime);
        }

        
    }
}