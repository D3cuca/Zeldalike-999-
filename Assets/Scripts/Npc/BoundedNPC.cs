using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : Sign
{
    public Vector3 DirectionVector;
    private Transform MyTransform;
    public float Speed;
    private Rigidbody2D MyRb;
    private Animator Anim;
    public Collider2D Bounds;
    public float MoveTimeSeconds;
    private bool isMoving;
    public float MinMoveTime;
    public float MaxMoveTime;


    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        MyTransform = GetComponent<Transform>();
        MyRb = GetComponent<Rigidbody2D>();
        ChangeDirection();
        Anim.SetBool("Walking", true);
        MoveTimeSeconds = Random.Range(MinMoveTime, MaxMoveTime);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!PlayerinRange)
        {
        Move();
        Anim.SetBool("Walking", true);
                MoveTimeSeconds -= Time.deltaTime;
            if (MoveTimeSeconds <= 0)
            {
                MoveTimeSeconds = Random.Range(MinMoveTime, MaxMoveTime);
                ChooseDifferentDirection();
            }
        }else
        {
            Anim.SetBool("Walking", false);
        }
    }

    private void Move()
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

    public void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                DirectionVector = Vector3.right;
                break;
            case 1:
                DirectionVector = Vector3.up;
                break;
            case 2:
                DirectionVector = Vector3.left;
                break;
            case 3:
                DirectionVector = Vector3.down;
                break;
            default:
                break;

        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        Anim.SetFloat("moveX", DirectionVector.x);
        Anim.SetFloat("moveY", DirectionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }

    private void ChooseDifferentDirection()
    {
        MoveTimeSeconds = Random.Range(MinMoveTime, MaxMoveTime);
        Vector3 temp = DirectionVector;
        ChangeDirection();
        int Loops = 0;
        while (temp == DirectionVector && Loops < 100)
        {
            Loops++;
            ChangeDirection();

        }
    }
}
