using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBottleObject : PowerUp
{
    public Inventory PlayerInventory;
    public float MagicGain = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory.CurrentMagic.RunTimeValue += MagicGain;
            PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
