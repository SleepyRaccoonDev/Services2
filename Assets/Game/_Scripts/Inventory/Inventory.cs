using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public enum ItemCategory { Weapon, Armor, Ammo, Quest, Other }
public enum ItemFamily { None, Sword, Arrow, Shield }
public enum DamageElement { None, Fire, Ice }

public class Inventory
{
    private Dictionary<Item, ReactiveVariable<int>> _items = new();

    public Inventory(int maxSize, Dictionary<Item, ReactiveVariable<int>> items = null)
    {
        if (items != null)
            _items = new Dictionary<Item, ReactiveVariable<int>>(items);

        if (maxSize > 0)
            MaxSize = maxSize;
    }

    public int MaxSize { get; }

    public int CurrentSize => _items.Values.Sum(v => v.Value);

    public IReadOnlyDictionary<Item, IReadOnlyVariable<int>> GetAllItems =>
        _items.ToDictionary(
            v => v.Key,
            v => (IReadOnlyVariable<int>)v.Value
        );

    public bool TryAdd(Item item, int count)
    {
        if (count <= 0)
            throw new Exception($"Добавляемое значение {count} для {item.Name} не корректно!");

        if (MaxSize == CurrentSize)
        {
            Debug.Log("Инвентарь переполнен!");
            return false;
        }

        if (CurrentSize + count > MaxSize)
        {
            var newCount = MaxSize - CurrentSize;
            Debug.Log($"{item.Name} добавлено только {newCount} шт.");
            return TryAdd(item, newCount);
        }

        if (_items.ContainsKey(item) == false)
            AddNewItemInInventory(item);

        _items[item].Value += count;

        return true;
    }

    public bool TryGetItemsBy(string name, int count, out Dictionary<Item, ReactiveVariable<int>> result)
    {
        result = null;

        if (string.IsNullOrEmpty(name) || count <= 0)
            return false;

        var items = _items.Where(kvp => kvp.Key.Name == name).ToList();

        if (items.Count == 0)
            return false;

        if (items.Count > 1)
            Debug.Log($"Ошибка инвентаря, в нём {items.Count} штук {name} предметов! Получен первый попавшийся!");
        
        Item item = _items.Keys.First(v => v.Name == name);

        if (_items[item].Value < count)
        {
            Debug.Log($"{item} изъято только {_items[item].Value} элементов");
            return TryGetItemsBy(name, _items[item].Value, out result);
        }

        _items[item].Value -= count;

        if (_items[item].Value <= 0)
            _items.Remove(item);

        result = new Dictionary<Item, ReactiveVariable<int>>();

        var value = new ReactiveVariable<int>(count);
        result.Add(item, value);

        return true;
    }

    private void AddNewItemInInventory(Item item)
    {
        _items.Add(item, new ReactiveVariable<int>(default));
    }

    public void PrintInventoryContents()
    {
        Debug.Log("В инвентаре содержится:");
        foreach (var item in _items)
        {
            Debug.Log($"{item.Key.Name} в количестве {item.Value.Value} шт.");
        }
    }
}