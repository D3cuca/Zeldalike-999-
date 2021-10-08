using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartObject : PowerUp
{
    public FloatValue HeartContainers;
    public FloatValue PlayerHealth;
    public float Amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerHealth.RunTimeValue += Amount;
            if(PlayerHealth.InitialValue > HeartContainers.RunTimeValue * 2f)
            {
                PlayerHealth.InitialValue = HeartContainers.RunTimeValue * 2f;
            }
            PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
