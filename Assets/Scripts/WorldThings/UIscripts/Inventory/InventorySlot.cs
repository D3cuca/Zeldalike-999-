using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Variables")]
    [SerializeField] private TextMeshProUGUI ItemNumberText;
    [SerializeField] private Image ItemImage;

    [Header("Variables From Items")]
    public InventoryItem ThisItem;
    public InventoryManager ThisManager;

    public void Setup(InventoryItem NewItem, InventoryManager NewManager)
    {
        ThisItem = NewItem;
        ThisManager = NewManager;
        if (ThisItem != null)
        {
            ItemImage.sprite = ThisItem.ItemImage;
            ItemNumberText.text = "" + ThisItem.NumberHeld;
        }
    }
public void ClickOn()
    {
        if (ThisItem)
        {
            ThisManager.SetUpDescriptionAndButton(ThisItem.ItemDescription, ThisItem.Usable, ThisItem);
        }
    }
}
