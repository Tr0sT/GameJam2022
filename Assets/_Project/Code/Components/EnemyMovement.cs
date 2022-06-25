#nullable enable
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Movement _movement = null!;

    private void OnEnable()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        var directionToPlayer = (PlayerMovement.Instance.transform.localPosition - transform.localPosition)
            .normalized;
        _movement.Direction = directionToPlayer;
    }
}