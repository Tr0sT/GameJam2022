#nullable enable
using Sirenix.Serialization;

public class EnemySettings : IEnemySettings
{
    [OdinSerialize]
    public int Health { get; set; }
    
    [OdinSerialize]
    public int Damage { get; set; }

    [OdinSerialize] 
    public IEnemyMovement EnemyMovement { get; set; } = new EnemyMovement();

}