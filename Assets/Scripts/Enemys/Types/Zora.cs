using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zora : Enemy
{
    public Transform Target;
    public float ChaseRadius;
    public float AttackRadius;
    public bool PlayerInRange;
    public GameObject Projectile;
    public float FireDelay;
    private float FireDelaySeconds;
    public bool CanFire = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        FireDelaySeconds -= Time.deltaTime;
        if (FireDelaySeconds <= 0)
        {
            CanFire = true;
            FireDelaySeconds = FireDelay;
        }
    }

    public virtual void CheckDistance()
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
            }
        }
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
        {
            PlayerInRange = false;
        }

    }
}
