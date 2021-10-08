using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLog : Log
{
    public BoxCollider2D Boundary;

    public override void CheckDistance()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius && Boundary.bounds.Contains(Target.transform.position)) //this last condition is to know if its within the limits
        {
            PlayerInRange = true;
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
            {

                StartCoroutine(WaitAndMoveCo()); //normally this coroutine was here (You added the Wait for seconds)

            }
        }
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius || !Boundary.bounds.Contains(Target.transform.position))
        {
            StartCoroutine(GoToSleep());
            PlayerInRange = false;
        }
    }
}
