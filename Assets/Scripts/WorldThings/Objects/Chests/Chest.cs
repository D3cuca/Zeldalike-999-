using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable

{
    [Header("Contents")]
    public Item Content;
    public Inventory PInventory;
    public bool IsOpen;
    public BoolValue StoredOpened;
    public InventoryWeaponItem ContentToAdd;
    public PlayerWeaponInventory PlayerWeaponInventory;

    [Header("Signals and Dialogue")]
    public SignalSender RaiseItem;
    public GameObject dialogBox;
    public Text DialogueText;

    [Header("Animation")]
    private Animator Anim;
    

    private void Start()
    {
        Anim = GetComponent<Animator>();
        IsOpen = StoredOpened.RuntimeValue;
        if (IsOpen)
        {
            Anim.SetBool("ChestOpen", true);
        }
    }

    void Update()
        {
        if (Input.GetButtonDown("Interact") && PlayerinRange)   // Keycode is used for non letter buttons
        {
            if (!IsOpen)
            {
                OpenChest();
            }
            else if (IsOpen)
            {
                ChestOpened();
            }
            else
            {
                PlayerinRange = false;
            }
        }
        }

    

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        DialogueText.text = ContentToAdd.ItemDescription;
        for(int i = 0; i < PlayerWeaponInventory.MyInventory.Count; i++)
        {
            if(PlayerWeaponInventory.MyInventory[i] == ContentToAdd)
            {
                PlayerWeaponInventory.MyInventory[i].Obtained = true;
            }
        }
        PInventory.AddItem(Content);
        PInventory.CurrentItem = Content;
        RaiseItem.Raise();
        Context.Raise();
        IsOpen = true;
        Anim.SetBool("ChestOpen", true);
        StoredOpened.RuntimeValue = IsOpen;
    }

    public void ChestOpened()
    {  
        dialogBox.SetActive(false);
        RaiseItem.Raise();

    }
    private void OnTriggerEnter2D(Collider2D other) //if the other enters bx collider area
    {
        if (other.CompareTag("Player") && !other.isTrigger && !IsOpen) // other = player tag
        {
            PlayerinRange = true;
            Context.Raise();
        }
    }
    public override void OnTriggerExit2D(Collider2D other)  // if other left bx collider area
    {
        if (other.CompareTag("Player") && !other.isTrigger && !IsOpen)   // other = player tag
        {
            PlayerinRange = false;
            Context.Raise();
        }
    }
}
