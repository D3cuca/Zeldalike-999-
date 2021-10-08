using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float MoveSpeed;
    public Vector2 Direction;

    [Header("LifeTime")]
    public float LifeTime;
    private float LifeTimeSeconds;
    [Header("Components")]
    public Rigidbody2D myRB;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        LifeTimeSeconds = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 InitialVelocity)
    {
        myRB.velocity = InitialVelocity * MoveSpeed;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Room"))
        {
            Debug.Log("exiting");

            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }


    

}
