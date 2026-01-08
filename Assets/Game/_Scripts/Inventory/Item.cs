using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public void Initialize(string name, ItemCategory itemCategory, ItemFamily itemFamily, DamageElement damageElement)
    {
        Name = name;

        ItemCategory = itemCategory;
        ItemFamily = itemFamily;
        DamageElement = damageElement;
    }

    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public ItemCategory ItemCategory { get; private set; }
    [field: SerializeField] public ItemFamily ItemFamily { get; private set; }
    [field: SerializeField] public DamageElement DamageElement { get; private set; }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not Item other)
            return false;

        return Name == other.Name
            && ItemCategory == other.ItemCategory
            && ItemFamily == other.ItemFamily
            && DamageElement == other.DamageElement;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Name,
            ItemCategory,
            ItemFamily,
            DamageElement
        );
    }
}