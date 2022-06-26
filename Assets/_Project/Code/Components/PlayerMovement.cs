#nullable enable
using Spine.Unity;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private SkeletonAnimation _animation = null!;
    [SerializeField] 
    private SkeletonAnimation _withSaw = null!, _withoutSaw = null!;
    
    private Joystick _joystick = null!;
    private Movement _movement = null!;
    public static PlayerMovement Instance { get; private set; } = null!;
    

    private bool dead = false;
    public bool jump = false;

    private void Update()
    {
        if (dead)
        {
            _movement.Direction = Vector2.zero;
            return;
        }
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput == Vector2.zero)
            moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        if (!jump)
        {
            if (moveInput == Vector2.zero)
                _animation.AnimationName = "Стоит";
            else
                _animation.AnimationName = "Идёт";
        }

        _movement.Direction = moveInput.normalized;

        if (Input.GetKey(KeyCode.Space))
            Jump();
    }

    private void OnEnable()
    {
        Instance = this;

        dead = false;
        jump = false;
        _movement = GetComponent<Movement>();
        if (GameWindow.Instance != null) _joystick = GameWindow.Instance.MovementJoystick;
        WithSaw(true);
    }

    public void WithSaw(bool with)
    {
        _withSaw.gameObject.SetActive(false);
        _withoutSaw.gameObject.SetActive(false);
        _animation = with ? _withSaw : _withoutSaw;
        _animation.gameObject.SetActive(true);
    }

    public void PlayDeathAnimation()
    {
        _animation.AnimationName = "Смерть";
        dead = true;
    }

    public void Jump()
    {
        if (!ShootController.Instance.CanJump())
            return;
        
        _animation.AnimationName = "Кувырок";
        GetComponent<Movement>().Speed *= 3;
        Invoke(nameof(EndJump), 1.5f);
        jump = true;
    }

    private void EndJump()
    {
        jump = false;
        GetComponent<Movement>().Speed /= 3;
    }
    
}