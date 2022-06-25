#nullable enable
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController Instance { get; private set; } = null!;

    [SerializeField] 
    private Rigidbody2D _rigidbody2D = null!;
    
    public bool pc;
    public float speed;
    
    private  Joystick _joystick = null!;

    
    public void Init(Joystick movementJoystick)
    {
        Instance = this;
        _joystick = movementJoystick;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput == Vector2.zero)
            moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        _rigidbody2D.velocity = moveInput.normalized * speed;
    }
}