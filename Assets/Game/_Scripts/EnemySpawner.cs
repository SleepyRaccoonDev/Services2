using UnityEngine;

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
            Create(_elfPrefab, randomElement1);

            var randomElement2 = GetRandomElement(_dragonProperties);
            Create(_dragonPrefab, randomElement2);

            var randomElement3 = GetRandomElement(_orkProperties);
            Create(_orkPrefab, randomElement3);
        }
    }

    public void Create<TEnemy, TSettings>(TEnemy enemyPrefab, TSettings settings)
        where TEnemy : Enemy<TSettings>
        where TSettings : EnemySettings
    {
        TEnemy instance = Instantiate(enemyPrefab);
        instance.Initialize(settings);
    }

    private T GetRandomElement<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}