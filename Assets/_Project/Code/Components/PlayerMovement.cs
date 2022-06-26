#nullable enable
using Spine.Unity;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation = null!;

    private Joystick _joystick = null!;
    private Movement _movement = null!;
    public static PlayerMovement Instance { get; private set; } = null!;

    private void Update()
    {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput == Vector2.zero)
            moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        if (moveInput == Vector2.zero)
            _animation.AnimationName = "Стоит";
        else
            _animation.AnimationName = "Идёт";
        _movement.Direction = moveInput.normalized;
    }

    private void OnEnable()
    {
        Instance = this;

        _movement = GetComponent<Movement>();
        if (GameWindow.Instance != null) _joystick = GameWindow.Instance.MovementJoystick;
    }
}