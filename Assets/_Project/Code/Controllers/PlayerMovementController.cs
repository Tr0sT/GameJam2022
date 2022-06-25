#nullable enable
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public bool pc;
    public float speed;
    
    [SerializeField]
    private  Joystick _joystick = null!;

    private Vector2 _moveInput;
    private Vector2 _moveVelocity;

    private void Update()
    {
        if (pc)
            _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else
            _moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        _moveVelocity = _moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        Vector3 add = _moveVelocity * Time.fixedDeltaTime;
        transform.localPosition += add;
    }
}