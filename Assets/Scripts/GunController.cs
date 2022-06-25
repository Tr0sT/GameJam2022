using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public float offset;
    public GameObject bullet;
    public Joystick joystick;
    public Transform shotPoint;

    private float timeBtwShorts;
    public float startTimeShots;
    public GameObject player;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
        {
            float rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }
        
    }

    public void btnShoot()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);
        timeBtwShorts = startTimeShots;

        if (timeBtwShorts <= 0)
        {

        }
        else
        {
            timeBtwShorts -= Time.deltaTime;
        }
    }
}
