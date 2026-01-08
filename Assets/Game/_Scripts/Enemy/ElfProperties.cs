using UnityEngine;

[System.Serializable]
public class ElfProperties
{
    [field: SerializeField, Min(0)] public float Agility { get; private set; }
    [field: SerializeField, Min(0)] public float Evasion { get; private set; }
    [field: SerializeField, Min(0)] public float Control { get; private set; }
}