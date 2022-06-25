#nullable enable
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; } = null!;
    
    private  Joystick _joystick = null!;
    private Movement _movement = null!;

    private void OnEnable()
    {
        Instance = this;
        
        _movement = GetComponent<Movement>();
        if (GameWindow.Instance != null)
        {
            _joystick = GameWindow.Instance.MovementJoystick;
        }
    }

    private void Update()
    {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput == Vector2.zero)
            moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        _movement.Direction = moveInput.normalized;
    }
}