#nullable enable
using Sirenix.Serialization;

public class EnemySettings : IEnemySettings
{
    [OdinSerialize] 
    public int Health { get; set; } = 3;

    [OdinSerialize] 
    public int Damage { get; set; } = 1;

    [OdinSerialize] 
    public int Speed { get; set; } = 600;
}