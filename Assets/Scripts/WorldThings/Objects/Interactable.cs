using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SignalSender Context;
    public bool PlayerinRange;


    private void OnTriggerEnter2D(Collider2D other) //if the other enters bx collider area
    {
        if (other.CompareTag("Player") && !other.isTrigger) // other = player tag
        {
            PlayerinRange = true;
            Context.Raise();
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other)  // if other left bx collider area
    {
        if (other.CompareTag("Player") && !other.isTrigger)   // other = player tag
        {
            PlayerinRange = false;
            Context.Raise();
        }
    }
}
