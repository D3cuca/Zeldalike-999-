using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPhysicalItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem Item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
    }

    void AddItemToInventory()
    {
        if (playerInventory && Item)
        {
            if (playerInventory.MyInventory.Contains(Item))
            {
                Item.NumberHeld += 1;
            }
            else
            {
                playerInventory.MyInventory.Add(Item);
                Item.NumberHeld += 1;
            }
        }
    }
}
