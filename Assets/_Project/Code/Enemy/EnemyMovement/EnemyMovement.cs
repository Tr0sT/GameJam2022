#nullable enable
using Sirenix.Serialization;
using UnityEngine;

public class EnemyMovement : IEnemyMovement
{
    [OdinSerialize] 
    public int Speed { get; set; } = 100;
    
    public void Move(Transform transform)
    {
        var directionToPlayer = (PlayerMovementController.Instance.transform.localPosition - transform.localPosition).normalized;

        var _moveVelocity = directionToPlayer * Speed;
        
        Vector3 add = _moveVelocity * Time.deltaTime;
        var localPosition = transform.localPosition;
        localPosition += add;
        var newPosition = new Vector2(Mathf.Clamp(localPosition.x, -1000, 1000),
            Mathf.Clamp(localPosition.y, -500, 500));
        transform.localPosition = newPosition;
    }
}