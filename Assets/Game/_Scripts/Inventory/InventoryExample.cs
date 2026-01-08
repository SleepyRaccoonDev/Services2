using System.Collections.Generic;
using UnityEngine;

public class InventoryExample : MonoBehaviour
{
    [SerializeField] private Item[] _itemsOnScene;

    private Inventory _inventory;

    private void Awake()
    {
        _inventory = new Inventory(10);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Item item = _itemsOnScene[Random.Range(0, _itemsOnScene.Length)];

            Debug.Log($"{item.Name}. Статус добавления - {_inventory.TryAdd(item, 3)}");

            _inventory.PrintInventoryContents();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Item item = _itemsOnScene[Random.Range(0, _itemsOnScene.Length)];

            Debug.Log($"{item.Name}. Статус удаления - {_inventory.TryGetItemsBy(item.Name, 4, out Dictionary<Item, ReactiveVariable<int>> result)}");

            _inventory.PrintInventoryContents();
        }
    }
}