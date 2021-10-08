using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item CurrentItem;
    public List<Item> items = new List<Item>();
    public int NumberOfKeys;
    public int Coins;
    public FloatValue MaxMagic;
    public FloatValue CurrentMagic;

    public void OnEnable()
    {
        CurrentMagic = MaxMagic;
        items.Clear(); // This is because otherwise if you obtain the bow, exit play mode and come back again, it will be there
    }

    public void ReduceMagic(float MagicCost)
    {
        CurrentMagic.RunTimeValue -= MagicCost;
    }

    public bool CheckItemPresence(Item Item)
    {
        if (items.Contains(Item))
         {
            return true;
        }
        return false;
    }

    public void AddItem( Item ItemToAdd)
    {
        if(ItemToAdd.IsKey)
        {
            NumberOfKeys++;
        }
        else
        {
            if (!items.Contains(ItemToAdd))
            {
                items.Add(ItemToAdd);
            }
        }
    }

}
