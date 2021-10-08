using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetButtonDown("Interact") && PlayerinRange)   // Keycode is used for non letter buttons
        {
            if (dialogBox.activeInHierarchy)    // active in hierarchy if the object is active
            {
                dialogBox.SetActive(false);
            } else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }
    public override void OnTriggerExit2D(Collider2D other)  // if other left bx collider area
    {
        if (other.CompareTag("Player") && !other.isTrigger)   // other = player tag
        {
            dialogBox.SetActive(false);
            PlayerinRange = false;
            Context.Raise();
        }
    }
}
