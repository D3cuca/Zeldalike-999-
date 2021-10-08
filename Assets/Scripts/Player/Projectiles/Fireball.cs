using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D myRb;
    public float MagicCost = 1;
    public bool Flying;
    [SerializeField] private Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Anim = this.gameObject.GetComponent<Animator>();
        StartCoroutine(CastAndFlyCo());

    }

    // Update is called once per frame
   public void Setup(Vector2 Velo, Vector3 Direction)
    {
        myRb.velocity = Velo.normalized * Speed;
        transform.rotation = Quaternion.Euler(Direction);  // how to do rotation, this is completed in the player script in choosearrowdirection()

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other != null)
        {
            Destroy(this.gameObject);
        }
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
    public IEnumerator CastAndFlyCo()
    {
        yield return new WaitForSeconds(0.33f);
        Anim.SetBool("Flying", true);
        //this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
