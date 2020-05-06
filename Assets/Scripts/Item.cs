using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public int itemID = 0;

    public enum ItemCategory
    {
        KeyItem,
        CombatItem,
        Weapon,
        Armor
    }
    public ItemCategory itemCategory;

    public Item(int _itemID)
    {
        itemID = _itemID;

        InitializeItem();
    }

    private void InitializeItem()
    {
        
    }
}
