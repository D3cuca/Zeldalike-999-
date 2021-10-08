using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool Active;
    public BoolValue StoredValue;
    public Sprite ActiveSprite;
    public SpriteRenderer MySprite;
    public Door ThisDoor;

    
    // Start is called before the first frame update
    void Start()
    {

        MySprite = GetComponent<SpriteRenderer>();
        Active = StoredValue.RuntimeValue;
        if (Active)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        Active = true;
        StoredValue.RuntimeValue = Active;
        ThisDoor.Opened();
        MySprite.sprite = ActiveSprite;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}
