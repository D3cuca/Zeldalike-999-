using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu] //[CreateAssetMenu(filename = "New Inventory", menuName = "Inventory/PlayerInventory")] 
public class PlayerWeaponInventory : ScriptableObject
{
    public List<InventoryWeaponItem> MyInventory = new List<InventoryWeaponItem>();

    public InventoryWeaponItem GetItem (string itemName)
    {
        for (int i = 0; i < MyInventory.Count; i++)
        {
            if (MyInventory[i].ItemName == itemName)
            {
                return MyInventory[i];
            }
        }
        return null;
    }
}
