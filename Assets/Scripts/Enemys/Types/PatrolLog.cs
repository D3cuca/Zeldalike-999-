using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] Path;
    public int CurrentPoint;
    public Transform CurrentGoal;
    public float RoundingDistance;

    void Start()
    {
        CurrentState = EnemyState.idle;
        anim = GetComponent<Animator>();
        Target = GameObject.FindWithTag("Player").transform;
        myRb = GetComponent<Rigidbody2D>();
        anim.SetBool("WakeUp", true);
    }
    public override void CheckDistance()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius)
        {
            PlayerInRange = true;
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
            {
                anim.SetBool("WakeUp", true);
                StartCoroutine(WaitAndMoveCo()); //normally this coroutine was here (You added the Wait for seconds)

            }
        }
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
        {
            if (Vector3.Distance(transform.position, Path[CurrentPoint].position) >RoundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, Path[CurrentPoint].position, WalkSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRb.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if (CurrentPoint == Path.Length - 1)
        {
            CurrentPoint = 0;
            CurrentGoal = Path[0];
        }
        else
        {
            CurrentPoint++;
            CurrentGoal = Path[CurrentPoint];
        }
    }

}
