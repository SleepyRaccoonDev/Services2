using UnityEngine;

public class Elf : Enemy
{
    private ElfProperties _elfProperties;

    public void Initialize(ElfProperties elfProperties)
    {
        _elfProperties = elfProperties;
    }
}