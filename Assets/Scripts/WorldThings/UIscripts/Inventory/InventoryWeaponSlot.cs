using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryWeaponSlot : MonoBehaviour
{
    [Header("UI Variables")]
    [SerializeField] private Image ItemImage;

    [Header("Variables From Items")]
    public InventoryWeaponItem ThisItem;
    public InventoryWeaponsManager ThisManager;

    public void Setup(InventoryWeaponItem NewItem, InventoryWeaponsManager NewManager)
    {
        ThisItem = NewItem;
        ThisManager = NewManager;
        if (ThisItem != null)
        {
            ItemImage.sprite = ThisItem.ItemImage;
        }
    }
public void ClickOn()
    {
        if (ThisItem)
        {
            ThisManager.SetUpDescriptionAndButton(ThisItem.ItemDescription, ThisItem);
        }
    }
}
