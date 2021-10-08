using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public Slider MagicSlider;
    public Inventory PlayerInventory;
    public FloatValue PlayerMagic;
    public FloatValue MaxMagic;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMagic.RunTimeValue = MaxMagic.RunTimeValue;
        MagicSlider.maxValue = MaxMagic.RunTimeValue;
        MagicSlider.value = PlayerMagic.RunTimeValue;
        PlayerInventory.CurrentMagic = PlayerMagic ;
    }

    private void Update()
    {
        PlayerMagic = PlayerInventory.CurrentMagic;
    }
    public void AddMagic()
    {
        MagicSlider.value = PlayerMagic.RunTimeValue;
        if(MagicSlider.value > MagicSlider.maxValue)
        {
            MagicSlider.value = MagicSlider.maxValue;
            PlayerMagic.RunTimeValue = PlayerInventory.MaxMagic.RunTimeValue;
        }
    }
    
    public void DecreaseMagic()
    {
        MagicSlider.value = PlayerMagic.RunTimeValue;
        if (MagicSlider.value < 0)
        {
            MagicSlider.value = 0;
            PlayerMagic.RunTimeValue = 0;
        }
    }
}
