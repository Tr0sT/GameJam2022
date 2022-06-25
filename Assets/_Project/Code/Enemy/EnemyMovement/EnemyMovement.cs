#nullable enable
using Sirenix.Serialization;
using UnityEngine;

public class EnemyMovement : IEnemyMovement
{
    [OdinSerialize] 
    public int Speed { get; set; } = 100;

    public void Move(Transform transform, Rigidbody2D rigidbody2D)
    {
        var directionToPlayer = (PlayerMovementController.Instance.transform.localPosition - transform.localPosition)
            .normalized;

        rigidbody2D.velocity = directionToPlayer * Speed;
    }

}