using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Player Things")]
    [SerializeField] private PlayerWeaponInventory Inventory;
    

    [Header("Ice Shield")]
    public bool IceActivated;
    public bool IceObtained;
    public Color IceColor;
    public Color DefaultColor;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Inventory.MyInventory.Count; i++)
        {
            if(Inventory.MyInventory[i].ItemName == "Ice Shield" && Inventory.MyInventory[i].Obtained == true)
            {
                IceObtained = true;
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {

        if (IceActivated == true)
        {
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
            {
                this.gameObject.transform.GetChild(i).tag = "IceDefense";
                this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = IceColor;
            }
        }
        else if (IceActivated == false)
        {
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
            {
                this.gameObject.transform.GetChild(i).tag = "Defense";
                this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = DefaultColor;
            }
        }
    }
    public void ChangeIce()
    {
        IceActivated = !IceActivated;
    }
}
