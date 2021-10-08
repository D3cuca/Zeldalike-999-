using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctorokTest : Enemy
{
    public Vector3 DirectionVector;
    private Transform MyTransform;
    public float Speed;
    private Rigidbody2D MyRb;
    private Animator Anim;
    public Collider2D Bounds;
    public float MoveTimeSeconds;
    private bool isMoving;
    public float MoveTime;
    public GameObject Projectile;
    public bool CanMove = true;


    // Start is called before the first frame update
    void Start()
    {
        CanMove = true;
        Anim = GetComponent<Animator>();
        MyTransform = GetComponent<Transform>();
        MyRb = GetComponent<Rigidbody2D>();
        ChangeDirection();
        Anim.SetBool("Moving", true);
        MoveTimeSeconds = MoveTime;
    }

    // Update is called once per frame
    public void Update()
    {
        if (State.CurrentState == States.Enemy)
        {
            Move();
            MoveTimeSeconds -= Time.deltaTime;
            if (MoveTimeSeconds <= 0)
            {
                MoveTimeSeconds = MoveTime;
                ChooseDifferentDirection();
            }

            else
            {
                Anim.SetBool("Moving", false);
            }
        }
    }

    private void Move()
    {
        if (CanMove)
        {
            Vector3 temp = MyTransform.position + DirectionVector * Speed * Time.deltaTime;
            if (Bounds.bounds.Contains(temp))
            {
                MyRb.MovePosition(temp);
            }
            else
            {
                ChangeDirection();
            }
        }

    }

    public void ChangeDirection()
    {
        
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                DirectionVector = Vector3.right;
                MyTransform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 1:
                DirectionVector = Vector3.up;
                MyTransform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 2:
                DirectionVector = Vector3.left;
                MyTransform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 3:
                DirectionVector = Vector3.down;
                MyTransform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                break;

        }
    }


    public void OnCollisionExit2D(Collision2D other)
    {
        

            ChooseDifferentDirection();
        
    }

    public void OnCollisionEnter(Collision collision)
    {
            Debug.Log("Collidinggg");
            
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DirectionVector = -DirectionVector;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        DirectionVector = -DirectionVector;
    }






    private void ChooseDifferentDirection()
    {
        FireCo();
        MoveTimeSeconds = MoveTime;
        Vector3 temp = DirectionVector;
        ChangeDirection();
        int Loops = 0;
        while (temp == DirectionVector && Loops < 100)
        {
            Loops++;
            ChangeDirection();

        }
    }
    public void FireCo()
    {
        GameObject current = Instantiate(Projectile, transform.position + DirectionVector, Quaternion.identity);
        current.GetComponent<Projectile>().Launch(DirectionVector * 4);

    }
}

