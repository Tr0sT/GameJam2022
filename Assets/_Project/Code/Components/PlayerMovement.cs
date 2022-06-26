#nullable enable
using Spine.Unity;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private SkeletonAnimation _animation = null!;
    [SerializeField] 
    private SkeletonAnimation _withSaw = null!, _withoutSaw = null!;
    
    [SerializeField] private float recoilDuration = 0.1f;
    [SerializeField] private AnimationCurve recoilVelocityOverTime;

    private float recoilStartTime = -1;
    private Vector2 recoilDirection;
    
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

        float recoilTime = GetRecoilTime();
        if (recoilTime < recoilDuration)
        {
            _movement.Direction = recoilDirection * recoilVelocityOverTime.Evaluate(recoilTime / recoilDuration);
            return;
        }

        _movement.Direction = moveInput.normalized;

        if (Input.GetKey(KeyCode.Space))
            Jump();
    }

    private float GetRecoilTime()
    {
        return Time.timeSinceLevelLoad - recoilStartTime;
    }

    private void OnEnable()
    {
        Instance = this;

        dead = false;
        jump = false;
        _movement = GetComponent<Movement>();
        if (GameWindow.Instance != null) _joystick = GameWindow.Instance.MovementJoystick;
        WithSaw(true);
        
        FindObjectOfType<ShootController>().ShotFired += OnShotFired;
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

        float recoilTime = GetRecoilTime();
        if (recoilTime < recoilDuration)
        {
            return;
        }

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
    
    private void OnShotFired(Vector2 dir)
    {
        recoilStartTime = Time.timeSinceLevelLoad;
        recoilDirection = -dir;
    }

}