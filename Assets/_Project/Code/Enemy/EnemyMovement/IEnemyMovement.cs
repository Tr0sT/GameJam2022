using UnityEngine;

public interface IEnemyMovement
{
    public int Speed { get; set; }
    public void Move(Transform transform);
}