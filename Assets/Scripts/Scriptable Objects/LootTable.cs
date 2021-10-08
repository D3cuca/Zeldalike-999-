using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Loot
{
    public PowerUp ThisLoot;
    public int LootChance;

}


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] Loots;

    public PowerUp LootPowerUp()
    {
        int CumulativeProbability = 0;
        int CurrentProbability = Random.Range(0, 100);
        for( int i =0; i < Loots.Length; i++)
        {
            CumulativeProbability += Loots[i].LootChance;
            if(CurrentProbability <= CumulativeProbability)
            {
                return Loots[i].ThisLoot;
            }
        }
        return null;
    }
}
