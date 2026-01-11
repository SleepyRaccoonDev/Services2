using UnityEngine;

[System.Serializable]
public class OrkProperties : EnemySettings
{
    [field: SerializeField, Min(0)] public float Strength { get; private set; }
    [field: SerializeField, Range(0, 1)] public float Armor { get; private set; }
    [field: SerializeField, Range(0, 1)] public float MageResistence { get; private set; }
    [field: SerializeField, Min(0)] public float AggressionRadius { get; private set; }
}