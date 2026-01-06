using System.Collections.Generic;
using UnityEngine;

public class Factory<T> where T : Component
{
    private readonly List<T> _allSpawns = new();
    private readonly Queue<T> _storedSpawns = new();

    public IReadOnlyList<T> SpawnedEntityes => _allSpawns;

    public T Get(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (_storedSpawns.Count <= 0)
            Create(prefab, position, rotation,parent);

        var instance = _storedSpawns.Dequeue();
        instance.gameObject.SetActive(true);

        return instance;
    }

    public void Return(T instance)
    {
        if (instance == null)
            return;

        instance.gameObject.SetActive(false);
        _storedSpawns.Enqueue(instance);
    }

    public void ClearAll()
    {
        foreach (var instance in _allSpawns)
        {
            if (instance != null)
                Object.Destroy(instance.gameObject);
        }

        _storedSpawns.Clear();
        _allSpawns.Clear();
    }

    private T Create(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        T instance = GameObject.Instantiate<T>(
            prefab,
            position,
            rotation,
            parent
        );

        _allSpawns.Add(instance);
        _storedSpawns.Enqueue(instance);

        return instance;
    }
}