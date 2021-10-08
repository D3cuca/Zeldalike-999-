using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLog : Log
{
    public GameObject Projectile;
    public float FireDelay;
    private float FireDelaySeconds;
    public bool CanFire = true;
    public void Update()
    {
        FireDelaySeconds -= Time.deltaTime;
        if(FireDelaySeconds <= 0)
        {
            CanFire = true;
            FireDelaySeconds = FireDelay;
        }
    }
    public override void CheckDistance()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius)
        {
            PlayerInRange = true;
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
            {
                if (CanFire)
                {
                    Vector3 TempVector = Target.transform.position - transform.position;
                    GameObject current = Instantiate(Projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(TempVector);
                    CanFire = false;
                }
                    ChangeState(EnemyState.walk);
                    anim.SetBool("WakeUp", true);
                


            }
        }
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
        {
            StartCoroutine(GoToSleep());
            PlayerInRange = false;
        }
    }
}
