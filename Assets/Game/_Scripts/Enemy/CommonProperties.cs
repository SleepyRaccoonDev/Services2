using System;
using UnityEngine;

[System.Serializable]
public class CommonProperties
{
    [field: SerializeField, Min(0)] public float Health { get; private set; }

    [field: SerializeField, Min(0)] public float Damage { get; private set; }

    [field: SerializeField, Min(0)] public float AttackSpeed { get; private set; }

    [field: SerializeField, Min(0)] public float DamageRange { get; private set; }

    [field: SerializeField, Min(0)] public float DelayBeforeAttack { get; private set; }
}