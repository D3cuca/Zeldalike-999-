using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    Key,
    Enemy,
    Button
}
public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType ThisDoorType;
    public bool Open = false;
    public Inventory PInventory;
    public SpriteRenderer UpSprite;
    public SpriteRenderer DownSprite;
    public BoxCollider2D BxCollider;



    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetButtonDown ("Interact"))
        {
            if (PlayerinRange && ThisDoorType == DoorType.Key)
            {
                if(PInventory.NumberOfKeys > 0)
                {
                    PInventory.NumberOfKeys--;
                    Opened();
                }
            }
        }
    }
    public void Opened()
    {
        UpSprite.enabled = false;
        DownSprite.enabled = false;
        Open = true;
        BxCollider.enabled = false;

    }
    public void Close()
    {
        UpSprite.enabled = true;
        DownSprite.enabled = true;
        Open = false;
        BxCollider.enabled = true;
    }
}
