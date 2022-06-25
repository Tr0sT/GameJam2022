#nullable enable

using System;

public interface IBullet
{
    public event Action<IBullet>? OnDestroy; 
    public void Init(IBulletSettings bulletSettings);

    void DeInit();
}