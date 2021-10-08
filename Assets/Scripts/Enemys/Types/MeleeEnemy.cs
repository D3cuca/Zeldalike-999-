using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{
    // Start is called before the first frame update
    void Start()
    {
        anim.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius)
        {
            PlayerInRange = true;
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
            {
                anim.SetBool("Walking", true);
                Vector3 temp = Vector3.MoveTowards(transform.position, Target.position, WalkSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRb.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius && Vector3.Distance(Target.position, transform.position) <= AttackRadius)
        {
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
            {

                StartCoroutine(AttackCo());
            }

        }
        else
        {
            anim.SetBool("Walking", false);
            CurrentState = EnemyState.idle;
        }

   }

    public IEnumerator AttackCo()
    {
        CurrentState = EnemyState.attack;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(1f);
        CurrentState = EnemyState.walk;
        anim.SetBool("Attacking", false);

    }

}
