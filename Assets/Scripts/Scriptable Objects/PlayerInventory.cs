using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu] //[CreateAssetMenu(filename = "New Inventory", menuName = "Inventory/PlayerInventory")] 



public class PlayerInventory : ScriptableObject
{
    [SerializeField] private Sprite LifePotion;
    [SerializeField] private Sprite ManaPotion;
    public List<InventoryItem> MyInventory = new List<InventoryItem>();

    public void PutImages()
    {
        GetItem("Health Potion").ItemImage = LifePotion;
        GetItem("Magic Potion").ItemImage = ManaPotion;
    }


    public InventoryItem GetItem (string itemName)
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
