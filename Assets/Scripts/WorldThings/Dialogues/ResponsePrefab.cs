using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponsePrefab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MyText;
    private int ChoiceValue;

    public void Setup(string NewDialog, int MyChoice)
    {
        MyText.text = NewDialog;
        ChoiceValue = MyChoice;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
