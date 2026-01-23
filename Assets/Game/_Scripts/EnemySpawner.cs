using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Range(0, 3)] private int _countOfEachEnemy = 3;

    [Space(10)]

    [SerializeField] private Dragon _dragonPrefab;
    [SerializeField] private Elf _elfPrefab;
    [SerializeField] private Ork _orkPrefab;

    [Space(10)]

    [field: SerializeField] private ElfProperties[] _elfProperties;
    [field: SerializeField] private OrkProperties[] _orkProperties;
    [field: SerializeField] private DragonProperties[] _dragonProperties;

    private void Awake()
    {
        for(int i = 0; i < _countOfEachEnemy; i++)
        {
            var randomElement1 = GetRandomElement(_elfProperties);
            Create(randomElement1, new Vector3(0,0,0));

            var randomElement2 = GetRandomElement(_dragonProperties);
            Create(randomElement2, new Vector3(-2, 0, 0));

            var randomElement3 = GetRandomElement(_orkProperties);
            Create(randomElement3, new Vector3(2, 0, 0));
        }
    }

    public Enemy Create(EnemySettings settings, Vector3 spawnPosition)
    {
        switch (settings)
        {
            case ElfProperties elfProperties:
                var elf = GameObject.Instantiate(_elfPrefab, spawnPosition, Quaternion.identity);
                elf.Initialize(elfProperties);
                return elf;

            case OrkProperties orkProperties:
                var ork = GameObject.Instantiate(_orkPrefab, spawnPosition, Quaternion.identity);
                ork.Initialize(orkProperties);
                return ork;

            case DragonProperties dragonProperties:
                var dragon = GameObject.Instantiate(_dragonPrefab, spawnPosition, Quaternion.identity);
                dragon.Initialize(dragonProperties);
                return dragon;

            default:
                throw new Exception($"Конфиги не найдены");
        }
    }

    private T GetRandomElement<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}