#nullable enable

using System;
using UnityEngine;

public interface IBullet
{
    public event Action<IBullet>? OnDestroy; 
    public void Init(Vector3 position, Vector3 direction, IBulletSettings bulletSettings);

    void DeInit();
}