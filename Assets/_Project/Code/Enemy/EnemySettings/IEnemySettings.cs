#nullable enable

public interface IEnemySettings
{
    public IEnemyMovement EnemyMovement { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
}
