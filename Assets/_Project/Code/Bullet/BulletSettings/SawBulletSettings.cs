#nullable enable
using Sirenix.Serialization;
using UnityEngine;

public class SawBulletSettings : IBulletSettings
{
    [OdinSerialize]
    public int Damage { get; set; } = 1;
    [OdinSerialize]
    public int MaxHitCount { get; set; } = 3;

    [OdinSerialize]
    public float speed = 1000;
    [OdinSerialize]
    public float lifetime;
}