using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public bool FireActivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void ChangeToFire()
    {
        FireActivated = !FireActivated;
    }
}
