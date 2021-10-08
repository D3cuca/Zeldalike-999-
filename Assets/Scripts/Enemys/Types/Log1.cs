using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log1 : Enemy
{
    public Rigidbody2D myRb;
    [Header("Target Variables")]
    public Transform Target;
    public float ChaseRadius;
    public float AttackRadius;
    public bool PlayerInRange;

    [Header("Animator")]
    public Animator anim;

    public GameObject DialogComponents;
    public GameObject EnemyComponents;
    public SignalSender ContextClue;

    
    // Start is called before the first frame update
    void Start()
    {
        spared = false;
        CurrentState = EnemyState.idle;
        anim = GetComponentInParent<Animator>();
        Target = GameObject.FindWithTag("Player").transform;
        myRb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spared == false)
        {
            CheckDistance();
        }
        else if (spared == true)
        {
            anim.SetBool("WakeUp", true);
            //ContextClue.Raise();
            DialogComponents.SetActive(true);
            EnemyComponents.SetActive(false);
            
        }
    }
    public virtual void CheckDistance()
    {
            if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius)
            {
                PlayerInRange = true;
                if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
                {

                    StartCoroutine(WaitAndMoveCo()); //normally this coroutine was here (You added the Wait for seconds)

                }
            }
            else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
            {
                StartCoroutine(GoToSleep());
                PlayerInRange = false;
            }

    }

    public void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            anim.SetFloat("moveY", 0);
            if (direction.x > 0)
            {
                anim.SetFloat("moveX", 1);

            }else if (direction.x < 0)
            {
                anim.SetFloat("moveX", -1);
            }
        }else if (Mathf.Abs (direction.x)< Mathf.Abs(direction.y))
        {
            anim.SetFloat("moveX", 0);
            if ( direction.y > 0)
            {
                anim.SetFloat("moveY", 1);
            }else if (direction.y < 0)
            {
                anim.SetFloat("moveY", -1);
            }
        }
    }
    public void ChangeState (EnemyState newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;
        }
    }
    public IEnumerator WaitAndMoveCo()
    {
        anim.SetBool("WakeUp", true);
        yield return new WaitForSeconds(0.7f);
        Vector3 temp = Vector3.MoveTowards(transform.position, Target.position, WalkSpeed * Time.deltaTime);
        changeAnim(temp - transform.position);
        myRb.MovePosition(temp);
        ChangeState(EnemyState.walk);

    }

    public IEnumerator GoToSleep ()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("WakeUp", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Defense"))
        {
            Debug.Log("Defense is touched");
            MonsterKarma++;

        }
        if (other.gameObject.CompareTag("IceDefense"))
        {
            Debug.Log("IceDefense is touched");
            MonsterKarma++;
            StartCoroutine(IcedCo());
        }
    }

    private IEnumerator IcedCo()
    {
        if (MonsterKarma <= (MaxKarma - 1))
        {
            float temp = WalkSpeed;
            Debug.Log("Is Being Iced");
            this.gameObject.GetComponentInParent<SpriteRenderer>().color = IcedColor;
            TriggerCollider.enabled = false;
            WalkSpeed = 0;
            anim.enabled = false;
            yield return new WaitForSeconds(IcedDuration);
            this.gameObject.GetComponentInParent<SpriteRenderer>().color = DefColor;
            TriggerCollider.enabled = true;
            WalkSpeed = temp;
            anim.enabled = true;
        }
        
    }
}
