using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainerObject : PowerUp
{
    public FloatValue HeartContainers;
    public FloatValue PlayerHealth;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HeartContainers.RunTimeValue += 1;
            PlayerHealth.RunTimeValue = HeartContainers.RunTimeValue * 2;
            PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
