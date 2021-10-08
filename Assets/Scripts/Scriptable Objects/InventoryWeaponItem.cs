using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu] //[CreateAssetMenu(filename = "New Item", menuName = "Inventory/Items")] 
public class InventoryWeaponItem : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemImage;
    public bool Obtained;
    public bool SetW;
    public bool SetX;
    public bool SetC;
    public bool SetV;
    public bool PassiveItem;

    public UnityEvent ThisEvent;

    public void Use()
    {
        if (Obtained)
        {
            ThisEvent.Invoke();
        }
    }

}
