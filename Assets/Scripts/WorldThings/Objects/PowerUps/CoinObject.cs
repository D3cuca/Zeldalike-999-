using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : PowerUp
{
    public Inventory PlayerInventory;
    // Start is called before the first frame update
    void Start()
    {
        PowerUpSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerInventory.Coins += 1;
            PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
