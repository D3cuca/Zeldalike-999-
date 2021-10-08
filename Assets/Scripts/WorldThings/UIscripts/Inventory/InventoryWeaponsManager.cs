using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryWeaponsManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerWeaponInventory PlayerWeaponGeneralInventory;
    [SerializeField] private GameObject BlankInventorySlot;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private GameObject UseWButton;
    [SerializeField] private GameObject UseXButton;
    [SerializeField] private GameObject UseCButton;
    [SerializeField] private GameObject IceButton;
    [SerializeField] private GameObject FireButton;
    public InventoryWeaponItem CurrentItem;


    private void Update()
    {
        if (CurrentItem && !CurrentItem.PassiveItem)
        {
            UseCButton.SetActive(true);
            UseWButton.SetActive(true);
            UseXButton.SetActive(true);
            if (CurrentItem.ItemName == "Basic Shield")
            {
                IceButton.SetActive(true);
                FireButton.SetActive(false);
                //once you have a function to fire and ice in both items, you can make hust one if statement

            }
            else if (CurrentItem.ItemName == "Sword")
            {
                FireButton.SetActive(true);
                IceButton.SetActive(false);
            }
            else
            {
                IceButton.SetActive(false);
                FireButton.SetActive(false);
            }
        }
        else
        {
            FireButton.SetActive(false);
            IceButton.SetActive(false);
            UseCButton.SetActive(false);
            UseWButton.SetActive(false);
            UseXButton.SetActive(false);
        }
    }

    public void SetDescription(string Description)
    {
        DescriptionText.text = Description;
    }

    public void MakeInventorySlots()
    {
        if (PlayerWeaponGeneralInventory)
        {
            for(int i = 0; i < PlayerWeaponGeneralInventory.MyInventory.Count; i++)
            {
                if (PlayerWeaponGeneralInventory.MyInventory[i].Obtained == true )
                {
                    GameObject Temp = Instantiate(BlankInventorySlot, InventoryPanel.transform.position, Quaternion.identity);
                    Temp.transform.SetParent(InventoryPanel.transform);
                    Temp.transform.localScale = BlankInventorySlot.transform.localScale;

                    InventoryWeaponSlot NewSlot = Temp.GetComponent<InventoryWeaponSlot>();

                    if (NewSlot)
                    {
                        NewSlot.Setup(PlayerWeaponGeneralInventory.MyInventory[i], this);
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
        SetDescription("");
    }

public void SetUpDescriptionAndButton(string NewDescriptionString, InventoryWeaponItem NewSelectedItem)
    {
        CurrentItem = NewSelectedItem;
        DescriptionText.text = NewDescriptionString;
    }
    void ClearInventorySlots()
    {
        for (int i = 0; i <InventoryPanel.transform.childCount; i++)
        {
            Destroy(InventoryPanel.transform.GetChild(i).gameObject);
        }
    }
    
    public void SetXButton()
    {
        if (CurrentItem)
        {
            for (int i = 0; i < PlayerWeaponGeneralInventory.MyInventory.Count; i++)
            {
                PlayerWeaponGeneralInventory.MyInventory[i].SetX = false;
            }
            CurrentItem.SetX = true;
            CurrentItem.SetC = false;
            CurrentItem.SetW = false;
            CurrentItem.SetV = false;
            ClearInventorySlots();
            MakeInventorySlots();
        }
    }
    public void SetWButton()
    {
        if (CurrentItem)
        {
            for (int i = 0; i < PlayerWeaponGeneralInventory.MyInventory.Count; i++)
            {
                PlayerWeaponGeneralInventory.MyInventory[i].SetW = false;
            }
            CurrentItem.SetW = true;
            CurrentItem.SetX = false;
            CurrentItem.SetC = false;
            CurrentItem.SetV = false;
            ClearInventorySlots();
            MakeInventorySlots();
        }
    }
    public void SetCButton()
    {
        if (CurrentItem)
        {
            for (int i = 0; i < PlayerWeaponGeneralInventory.MyInventory.Count; i++)
            {
                PlayerWeaponGeneralInventory.MyInventory[i].SetC = false;
            }
            CurrentItem.SetC = true;
            CurrentItem.SetX = false;
            CurrentItem.SetW = false;
            CurrentItem.SetV = false;
            ClearInventorySlots();
            MakeInventorySlots();
        }
    }
    public void SetVButton()
    {
        if (CurrentItem)
        {
            for (int i = 0; i < PlayerWeaponGeneralInventory.MyInventory.Count; i++)
            {
                PlayerWeaponGeneralInventory.MyInventory[i].SetV = false;
            }
            CurrentItem.SetC = false;
            CurrentItem.SetX = false;
            CurrentItem.SetW = false;
            CurrentItem.SetV = true;
            ClearInventorySlots();
            MakeInventorySlots();
        }
    }
}
