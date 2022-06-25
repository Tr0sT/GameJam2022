#nullable enable
using UnityEngine;

public class SawBulletSettings : IBulletSettings
{
    public Vector3 StartPosition { get; set; }
    public Vector3 StartDirection { get; set; }
    public int MaxHitCount { get; set; } = 3;

    public float speed = 1000;
    public float lifetime;
    public float distance = 100;
    public int damage = 1;
    public LayerMask whatIsSolid;

}