using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory PlayerGeneralInventory;
    [SerializeField] private GameObject BlankInventorySlot;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private GameObject UseButton;
    public InventoryItem CurrentItem;
    public UnityEvent Health;
    public UnityEvent Magic;

    public void Update()
    {
        if (CurrentItem)
        {
            if (CurrentItem.ItemName == "Health Potion")
            {
                CurrentItem.ThisEvent = Health;
            }
            else if (CurrentItem.ItemName == "Magic Potion")
            {
                CurrentItem.ThisEvent = Magic;
            }
        }
    }

    public void SetTextAndButton(string Description, bool ButtonActive)
    {
        DescriptionText.text = Description;
        if (ButtonActive)
        {
            UseButton.SetActive(true);
        }
        else
        {
            UseButton.SetActive(false);
        }
    }

    public void MakeInventorySlots()
    {
        if (PlayerGeneralInventory)
        {
            for(int i = 0; i < PlayerGeneralInventory.MyInventory.Count; i++)
            {
                if (PlayerGeneralInventory.MyInventory[i].NumberHeld > 0 )
                {
                    GameObject Temp = Instantiate(BlankInventorySlot, InventoryPanel.transform.position, Quaternion.identity);
                    Temp.transform.SetParent(InventoryPanel.transform);
                    Temp.transform.localScale = BlankInventorySlot.transform.localScale;

                    InventorySlot NewSlot = Temp.GetComponent<InventorySlot>();

                    if (NewSlot)
                    {
                        NewSlot.Setup(PlayerGeneralInventory.MyInventory[i], this);
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void OnEnable() //Onenable works every time the object is activated, start only runs once the first time
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

public void SetUpDescriptionAndButton(string NewDescriptionString, bool IsButtonUsable, InventoryItem NewSelectedItem)
    {
        CurrentItem = NewSelectedItem;
        DescriptionText.text = NewDescriptionString;
        UseButton.SetActive(IsButtonUsable);
    }
    void ClearInventorySlots()
    {
        for (int i = 0; i <InventoryPanel.transform.childCount; i++)
        {
            Destroy(InventoryPanel.transform.GetChild(i).gameObject);
        }
    }
    
    public void UseButtonPressed()
    {
        if (CurrentItem)
        {
            CurrentItem.Use();
            ClearInventorySlots();
            MakeInventorySlots();
            if (CurrentItem.NumberHeld == 0)
            {
                SetTextAndButton("", false);
            }
        }
    }
}
