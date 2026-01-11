using UnityEngine;

[System.Serializable]
public class DragonProperties : EnemySettings
{
    [field: SerializeField, Min(0)] public float Mana { get; private set; }
    [field: SerializeField, Min(0)] public float Fear { get; private set; }
    [field: SerializeField, Min(0)] public float Greed { get; private set; }
}