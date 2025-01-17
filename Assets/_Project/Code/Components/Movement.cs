﻿#nullable enable
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 Direction;

    public int Speed;
    
    [SerializeField] 
    private Rigidbody2D _rigidbody2D = null!;
    
    private void Update()
    {
        if (_rigidbody2D.bodyType == RigidbodyType2D.Static)
        {
            return;
        }
        _rigidbody2D.velocity = Direction * Speed;
    }
}