using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Jumpstate
{
    Jumping,
    Falling,
    Waiting,
    CanJump
}
public class Tektite : Enemy
{
    [SerializeField] private Transform Target;
    public float ChaseRadius;
    public float AttackRadius;
    public bool PlayerInRange;
    private Animator Anim;
    private Rigidbody2D Rb;
    public bool isJumping;
    public Jumpstate CurrState;
    public Vector3 LandingSpot;
    public Vector3 JumpHeight;
    public float FallingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Anim = this.gameObject.GetComponent<Animator>();
        Rb = this.gameObject.GetComponent<Rigidbody2D>();
        CurrState = Jumpstate.CanJump;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius)
        {
            PlayerInRange = true;
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk && CurrentState != EnemyState.stagger)
            {

                StartCoroutine(JumpCo()); //normally this coroutine was here (You added the Wait for seconds)

            }
        }
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
        {
            PlayerInRange = false;
            this.gameObject.layer = 9;
            Anim.SetBool("Idling", true);
            CurrState = Jumpstate.CanJump;

        }

    }

    private IEnumerator JumpCo()
    {

        if( CurrState == Jumpstate.CanJump)
        {
            Anim.SetBool("Idling", false);
            Anim.SetTrigger("Jumping");
            yield return new WaitForSeconds(1f);
            this.gameObject.layer = 4;
            SetJumpHeight();
            CurrState = Jumpstate.Jumping;

        }
        if (CurrState == Jumpstate.Jumping)
        {
            if (!Mathf.Approximately(transform.position.y, JumpHeight.y) || !Mathf.Approximately(transform.position.x, JumpHeight.x))
            { //transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2 (2, 2), Time.deltaTime);
                Vector3 temp = Vector3.MoveTowards(transform.position, JumpHeight, WalkSpeed * Time.deltaTime);
                Rb.MovePosition(temp);
            }
            if (Mathf.Approximately(transform.position.y, JumpHeight.y) || Mathf.Approximately(transform.position.x, JumpHeight.x))
            {
                SetLandingSpot();
                CurrState = Jumpstate.Falling;
            }
        }
        if (CurrState == Jumpstate.Falling)
        {
           
            Vector3 tempi = Vector3.MoveTowards(transform.position, LandingSpot, WalkSpeed * Time.deltaTime);
            Rb.MovePosition(tempi);
            if (Mathf.Approximately( transform.position.x, LandingSpot.x))
            {
                this.gameObject.layer = 9;
                CurrState = Jumpstate.Waiting;
                Anim.SetBool("Idling", true);
                yield return new WaitForSeconds(7);
                CurrState = Jumpstate.CanJump;
            }
            
        }
    }

    private void SetLandingSpot()
    {
        LandingSpot = Target.position;
    }
    
    private void SetJumpHeight()
    {
        if (Target.position.x > 0 && transform.position.x > 0)
        {
            if (Target.position.x > transform.position.x)
            {
                JumpHeight = new Vector3(((Target.position.x - transform.position.x) / 2) + transform.position.x, Target.position.y + 3);

            }
            else if (Target.position.x < transform.position.x)
            {
                JumpHeight = new Vector3(((transform.position.x - Target.position.x) / 2) + Target.position.x, Target.position.y + 3);

            }
        }

        if (Target.position.x < 0 && transform.position.x < 0)
        {
            if (Target.position.x < transform.position.x)
            {
                JumpHeight = new Vector3(((Target.position.x - transform.position.x) / 2) + transform.position.x, Target.position.y + 3);

            }
            else if (Target.position.x > transform.position.x)
            {
                JumpHeight = new Vector3(((transform.position.x - Target.position.x) / 2) + Target.position.x, Target.position.y + 3);

            }
        }



    }
}
