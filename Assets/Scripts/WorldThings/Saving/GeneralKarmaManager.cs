using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GeneralKarmaManager : MonoBehaviour
{
    public int KarmaCounter;
    public List<string> DeceasedNames = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KarmaUp()
    {
        KarmaCounter++;
    }

    public void AddNameToList(string NameToAdd)
    {
        DeceasedNames.Add(NameToAdd);
    }
}
