using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D myRb;
    public float MagicCost = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   public void Setup(Vector2 Velo, Vector3 Direction)
    {
        myRb.velocity = Velo.normalized * Speed;
        transform.rotation = Quaternion.Euler(Direction);  // how to do rotation, this is completed in the player script in choosearrowdirection()

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Others"))
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerExit2D (Collider2D room)
    {
        if(room.gameObject.CompareTag("Room"))
        {
            Destroy(this.gameObject);
        }
    }
}
