using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{
    [field: SerializeField] public bool IsDead { get; private set; }

    [field: SerializeField] public float LifeTime { get; private set; }

    public void SetLifeTime(float value) => LifeTime = value;

    private void Update()
    {
        if (LifeTime > 0)
            LifeTime -= Time.deltaTime;
    }

    public void Kill()
    {
        GameObject.Destroy(this.gameObject);
    }
}