﻿using NuclearBand;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public static ShootController Instance { get; private set; }

    public event System.Action<Vector2> ShotFired;

    public bool pc;
    public float offset;
    public GameObject circle;
    public Transform crosshair;
    public Sprite noArmor;
    public Sprite yesArmor;
    private Joystick _joystick;
    public Transform shotPoint;

    private float timeBtwShorts;
    private float rotZ;
    private Vector3 difference;

    public int StartBulletsCount = 3;

    public float startTimeShots;

    private int _curBulletCount;

    public void Init(Joystick shootJoystick)
    {
        _joystick = shootJoystick;

        Instance = this;

        _curBulletCount = StartBulletsCount;
    }
    
    void Update()
    {
        crosshair.transform.localScale = new Vector3(1f, _joystick.Direction.magnitude, 1f);

        if (!pc && Mathf.Abs(_joystick.Horizontal) > 0.1f || Mathf.Abs(_joystick.Vertical) > 0.1f)
        {
            rotZ = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
        }
        else
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        
        if (timeBtwShorts > 0)
        {
            timeBtwShorts -= Time.deltaTime;
        }
        if (pc && Input.GetMouseButton(0))
        {
            Shoot();
            circle.GetComponent<Animator>().enabled = true;
            circle.GetComponent<Animator>().Play("circleAnim", 0, 0.25f);
        }
        if(_curBulletCount <= 0)
        {
            circle.GetComponent<SpriteRenderer>().sprite = noArmor;
        }
        else
        {
            circle.GetComponent<SpriteRenderer>().sprite = yesArmor;
        }
    }

    public void Shoot()
    {
        if (timeBtwShorts > 0 || _curBulletCount <= 0)
            return;
        
        BulletSpawnController.Instance.SpawnBullet(shotPoint.position, transform.rotation * Vector3.up, new SawBulletSettings());
        AudioManager.PlaySound("Выстрел2");
        
        _curBulletCount--;
        if (_curBulletCount <= 0)
        {
            PlayerMovement.Instance.WithSaw(false);
            PlayerMovement.Instance.WithSaw(false);
        }
        timeBtwShorts = startTimeShots;

        ShotFired?.Invoke(transform.rotation * Vector3.up);
    }

    public bool CanJump()
    {
        return _curBulletCount <= 0 && !PlayerMovement.Instance.jump;
    }

    public void PickupSaw()
    {
        _curBulletCount++;
        PlayerMovement.Instance.WithSaw(true);
    }
}