using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool pc = true;
    public float offset;
    public GameObject bullet;
    public Joystick joystick;
    public Transform shotPoint;

    private float timeBtwShorts;
    private float rotZ;
    private Vector3 difference;

    public float startTimeShots;
    public GameObject player;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!pc && Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
        {
            rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;  
        }
        else
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (Input.GetMouseButton(0) && pc)
        {
            btnShoot();
        }
        else if (!pc)
        {
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                btnShoot();
            }
        }
    }

    public void btnShoot()
    {
        if (timeBtwShorts <= 0)
        {
            Instantiate(bullet, shotPoint.position, transform.rotation);
            timeBtwShorts = startTimeShots;
        }
        else
        {
            timeBtwShorts -= Time.deltaTime;
        }


    }
}
