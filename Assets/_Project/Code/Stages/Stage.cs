using System.Collections.Generic;

public class Stage
{
    public int Time;
    public int MaxEnemiesCount;
    public float SpawnDelay;
    public List<IEnemySettings> Enemies;
}