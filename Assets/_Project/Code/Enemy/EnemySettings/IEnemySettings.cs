#nullable enable
using UnityEngine;

public interface IEnemySettings
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public int Speed { get; set; }
}
