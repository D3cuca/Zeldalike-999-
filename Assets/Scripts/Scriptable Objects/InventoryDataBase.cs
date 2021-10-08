using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class InventoryDataBase : ScriptableObject
{
    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();
    [SerializeField] private List<InventoryWeaponItem> WeaponItems = new List<InventoryWeaponItem>();

    public InventoryItem GetItem(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ItemName == itemName)
            {
                return items[i];
            }
        }
        return null;
    }
    public InventoryWeaponItem GetWeaponItem(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ItemName == itemName)
            {
                return WeaponItems[i];
            }
        }
        return null;
    }
}
