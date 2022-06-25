using UnityEngine;

public class ShootController : MonoBehaviour
{
    public bool pc;
    public float offset;
    private Joystick _joystick;
    public Transform shotPoint;

    private float timeBtwShorts;
    private float rotZ;
    private Vector3 difference;

    public float startTimeShots;

    public void Init(Joystick shootJoystick)
    {
        _joystick = shootJoystick;
    }
    
    void Update()
    {
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
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (timeBtwShorts > 0)
            return;
        
        BulletSpawnController.Instance.SpawnBullet(shotPoint.position, transform.rotation * Vector3.up, new SawBulletSettings());
        timeBtwShorts = startTimeShots;
    }
}