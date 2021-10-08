using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryReactions : MonoBehaviour
{
    [Header("Magic Reaction")]
    public FloatValue PlayerMagic;
    public SignalSender MagicSignal;

    [Header("Health Reaction")]
    public FloatValue PlayerHealth;
    public SignalSender HealthSignal;



    public void UseMagicPotion(int AmountToIncrease)
    {
        PlayerMagic.RunTimeValue += AmountToIncrease;
        MagicSignal.Raise();
    }
    
    public void UseHealthPotion(int AmountToIncrease)
    {
        PlayerHealth.RunTimeValue += AmountToIncrease;
        HealthSignal.Raise();
    }
}
