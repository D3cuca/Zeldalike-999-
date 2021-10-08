using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaHat : Enemy
{
    private Animator Anim;
    public Transform Target;
    public float ChaseRadius;
    public bool PlayerInRange;
    private Rigidbody2D myRb;
    public bool ChasingPlayer;

    // Don't know what's better, everything in one Corutine or separate Corutines or you can everything in one animation
    void Start()
    {
        Anim = this.gameObject.GetComponent<Animator>();
        myRb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckDistance();
        if(ChasingPlayer)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, Target.position, WalkSpeed * Time.deltaTime);
            myRb.MovePosition(temp);
        }
    }


    public void CheckDistance()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius)
        {
            PlayerInRange = true;
            StartCoroutine(FlyingCo());

            
        }
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius && ChaseRadius !=0)
        {
            StartCoroutine(SleepCo());
        }

    }

    public IEnumerator FlyingCo()
    {
        yield return new WaitForSeconds(0.75f);
        myRb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 2), Time.deltaTime));
        yield return new WaitForSeconds(1.30f);
        ChasingPlayer = true;
        yield return new WaitForSeconds(2.66f);
        ChasingPlayer = false;
    }

    public IEnumerator WakeCo()
    {
        float tempi = ChaseRadius;
        Anim.SetBool("TakingFlight", true);
        Debug.Log("Its Flying");
        if(Anim.GetBool("TakingFlight") == true && Anim.GetBool("Flying") != true)
        {
            yield return new WaitForSeconds(1.8f);
            myRb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y +2), Time.deltaTime));
        }
        yield return new WaitForSeconds(13f);
        Anim.SetBool("TakingFlight", false);
        ChaseRadius = 0;
        yield return new WaitForSeconds(7f);
        ChaseRadius = tempi;
    }
    public IEnumerator FlyCo()
    {
        yield return new WaitForSeconds(2.4f);
        Anim.SetBool("Flying", true);
        yield return new WaitForSeconds(10f);
        Anim.SetBool("Flying", false);
    }
    private IEnumerator SleepCo ()
    {
        PlayerInRange = false;
        Anim.SetBool("Flying", false);
        yield return new WaitForSeconds(2f);
        Anim.SetBool("TakingFlight", false);
    }
}
