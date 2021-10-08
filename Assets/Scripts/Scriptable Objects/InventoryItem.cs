using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu] //[CreateAssetMenu(filename = "New Item", menuName = "Inventory/Items")] 
public class InventoryItem : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    [SerializeField] public Sprite ItemImage;
    public int NumberHeld;
    public bool Usable;
    public bool Unique;
    public UnityEvent ThisEvent;

    public void Use()
    {
        if (NumberHeld > 0)
        {
            ThisEvent.Invoke();
        }
    }

    public void DecreaseAmount(int AmountToDecrease)
    {
        NumberHeld -= AmountToDecrease;
        if(NumberHeld < 0)
        {
            NumberHeld = 0;
        }
    }
}
