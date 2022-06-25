using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public int health;
    public Joystick joystick;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void ChengeHealth(int helthValue)
    {
        health += helthValue;
    }
}