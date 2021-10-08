using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Playerstate
{
    idle,
    walk,
    attack,
    interact,
    stagger,
    block, 
    jump
}

public class PlayerMovement : MonoBehaviour
{
    [Header("State")]
    public Playerstate CurrentState;
    public SpriteRenderer ReceivedItemSprite;

    [Header("Movement")]
    public float speed;
    private Vector3 change;
    public Vector2Value StartingPosition;

    [Header("Components")]
    public Inventory OwnInventory;
    private Rigidbody2D myRigidbody;
    private Animator animator;
    public Collider2D TriggerCollider;
    public SpriteRenderer MySprite;
    public Transform MyTransfrom;
    public BoxCollider2D Hitbox;
    public BoxCollider2D PlayerPhysics;

    [Header("Health")]
    public FloatValue CurrentHealth;
    public SignalSender PlayerHealthSignal;

    [Header("Signals")]

    public SignalSender PlayerHit;
    public SignalSender DecreaseMagic;

    [Header("Projectile Stuff")]
    public GameObject projectile;
    public GameObject FireballObject;
    public Item Bow;

    [Header("Flash / Invulnerabiity")]
    public Color FlashColor;
    public Color DefColor;
    public float Flashduration;
    public int FlashQuantity;

    [Header("Inventory Stuff")]
    [SerializeField] private PlayerWeaponInventory MYGeneralWeaponInventory;

    [Header("Defense Stuff")]
    private bool CanLiftShield = true;


    // Start is called before the first frame update
    void Start()
    {
        CurrentState = Playerstate.walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        MyTransfrom = GetComponent<Transform>();
        transform.position = StartingPosition.InitialValue;
    }


    // Update is called once per frame
    void Update()
    {   PlayerHealthSignal.Raise(); // this shouldn't be happening everyframe, it's used for the signal to update hearts at the start
        change = Vector3.zero; //set to zero
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (CurrentState == Playerstate.interact) 
        {
            return;
        }
        if (Input.GetButtonDown("WButton") && CurrentState != Playerstate.attack && CurrentState!= Playerstate.stagger)
        {
            for (int i = 0; i < MYGeneralWeaponInventory.MyInventory.Count; i++)
            {
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Sword") && MYGeneralWeaponInventory.MyInventory[i].SetW == true)
                {
                    StartCoroutine(AttackCo());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Bow") && MYGeneralWeaponInventory.MyInventory[i].SetW == true)
                {
                    StartCoroutine(Attack2Co());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Basic Shield") && MYGeneralWeaponInventory.MyInventory[i].SetW == true)
                {
                    StartCoroutine(DefenseCo());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Jump Boots") && MYGeneralWeaponInventory.MyInventory[i].SetW == true)
                {
                    StartCoroutine(JumpCo());
                }
            }
        }
        else if (Input.GetButtonDown("XButton") && CurrentState != Playerstate.attack && CurrentState != Playerstate.stagger)
        {
            for (int i = 0; i < MYGeneralWeaponInventory.MyInventory.Count; i++)
            {
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Bow") && MYGeneralWeaponInventory.MyInventory[i].SetX == true)
                {
                    StartCoroutine(Attack2Co());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Sword") && MYGeneralWeaponInventory.MyInventory[i].SetX == true)
                {
                    StartCoroutine(AttackCo());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Basic Shield") && MYGeneralWeaponInventory.MyInventory[i].SetX == true)
                {
                    StartCoroutine(DefenseCo());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Jump Boots") && MYGeneralWeaponInventory.MyInventory[i].SetX == true)
                {
                    StartCoroutine(JumpCo());
                }
            }
        }
        else if (Input.GetButton("CButton") && CurrentState != Playerstate.attack && CurrentState != Playerstate.stagger)
        {
            for (int i = 0; i < MYGeneralWeaponInventory.MyInventory.Count; i++)
            {
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Basic Shield") && MYGeneralWeaponInventory.MyInventory[i].SetC == true)
                {
                    StartCoroutine(DefenseCo());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Bow") && MYGeneralWeaponInventory.MyInventory[i].SetC == true)
                {
                    StartCoroutine(Attack2Co());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Sword") && MYGeneralWeaponInventory.MyInventory[i].SetC == true)
                {
                    StartCoroutine(AttackCo());
                }
                if (MYGeneralWeaponInventory.MyInventory[i].ItemName == ("Jump Boots") && MYGeneralWeaponInventory.MyInventory[i].SetC == true)
                {
                    StartCoroutine(JumpCo());
                }
            }
        }
        else if (Input.GetButtonDown("VButton") && CurrentState != Playerstate.attack && CurrentState != Playerstate.stagger)
        {
            StartCoroutine(JumpCo());
        }
        else if (CurrentState == Playerstate.walk || CurrentState == Playerstate.idle)
            {
                UpdateAnimationAndMove();
            }

    }

    private InventoryWeaponItem CheckItems(string ItemToCheck)
    {
        for (int i = 0; i < MYGeneralWeaponInventory.MyInventory.Count; i ++)
        {
            if(MYGeneralWeaponInventory.MyInventory[i].ItemName == ItemToCheck)
            {
                return MYGeneralWeaponInventory.MyInventory[i];
            }
            
        }
        return null; 
    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        CurrentState = Playerstate.attack;
        if(CheckItems("Fireball").Obtained && this.gameObject.GetComponentInChildren<Sword>().FireActivated)
        {
            Debug.Log("Firing");
            MakeFireball();
        }
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        if (CurrentState != Playerstate.interact)
        {
            CurrentState = Playerstate.walk;
        }
    }

    private IEnumerator Attack2Co()
    {
        CurrentState = Playerstate.attack;
        yield return null;
        MakeArrow();
        yield return new WaitForSeconds(.33f);
        if (CurrentState != Playerstate.interact)
        {
            CurrentState = Playerstate.walk;
        }
    }

    private IEnumerator DefenseCo()
    {
        if (!CheckItems("Bronze Gauntlet").Obtained && !CheckItems("Silver Gauntlet").Obtained)
        {
            if (CanLiftShield)
            {
                animator.SetBool("Blocking", true);
                this.gameObject.tag = "Defense";
                CurrentState = Playerstate.block;
                speed = 0;
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(LiftShield());
                animator.SetBool("Blocking", false);
                this.gameObject.tag = "Player";
                yield return new WaitForSeconds(0.5f);
                if (CurrentState != Playerstate.interact)
                {
                    CurrentState = Playerstate.walk;
                }
                speed = 4;
            }
        }
        else if (CheckItems("Bronze Gauntlet").Obtained && !CheckItems("Silver Gauntlet").Obtained)
        {
            if (CanLiftShield)
            {
                animator.SetBool("Blocking", true);
                this.gameObject.tag = "Defense";
                CurrentState = Playerstate.block;
                speed = 0;
                UpdateAnimationAndMove();
                yield return new WaitForSeconds(0.01f);
                if ((CheckItems("Basic Shield").SetC && Input.GetButtonUp("CButton"))
                || (CheckItems("Basic Shield").SetX && Input.GetButtonUp("XButton"))
                || (CheckItems("Basic Shield").SetW && Input.GetButtonUp("WButton")))
                {
                    Debug.Log("button released");
                    StartCoroutine(LiftShield());
                    animator.SetBool("Blocking", false);
                    this.gameObject.tag = "Player";
                    yield return new WaitForSeconds(0.5f);
                    if (CurrentState != Playerstate.interact)
                    {
                        CurrentState = Playerstate.walk;
                    }
                    speed = 4;
                }

            }
            else speed = 4;
        }
        else if (CheckItems("Silver Gauntlet").Obtained)
        {
            animator.SetBool("BlockMoving", true);
            this.gameObject.tag = "Defense";
            CurrentState = Playerstate.block;
            speed = 2;
            UpdateAnimationAndMove();
            yield return new WaitForSeconds(0.0001f);
            if ((CheckItems("Basic Shield").SetC && Input.GetButtonUp("CButton"))
            || (CheckItems("Basic Shield").SetX && Input.GetButtonUp("XButton"))
            || (CheckItems("Basic Shield").SetW && Input.GetButtonUp("WButton")))
            {
                Debug.Log("button released");
                StartCoroutine(LiftShield());
                animator.SetBool("BlockMoving", false);
                this.gameObject.tag = "Player";
                if (CurrentState != Playerstate.interact)
                {
                    CurrentState = Playerstate.walk;
                }
                speed = 4;
            }
        }
    }

    private void MakeArrow()
    {
        if (OwnInventory.CurrentMagic.RunTimeValue > 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            OwnInventory.ReduceMagic(arrow.MagicCost);
            DecreaseMagic.Raise();
        }
    }

    private void MakeFireball()
    {
        if (OwnInventory.CurrentMagic.RunTimeValue > 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Fireball Fireball = Instantiate(FireballObject, new Vector3 (transform.position.x + temp.x , transform.position.y + temp.y , transform.position.z), Quaternion.identity).GetComponent<Fireball>();
            Fireball.Setup(temp, ChooseArrowDirection());
            OwnInventory.ReduceMagic(Fireball.MagicCost);
            DecreaseMagic.Raise();
        }
    }

    private IEnumerator JumpCo()
    {
        CurrentState = Playerstate.jump;
        animator.SetBool("Jumping", true);
        yield return new WaitForSeconds(0.15F);
            // you can use layers to make the player only be in contact with limits and not disabming our box colliders
            if (change != Vector3.zero)
            {
                MoveCharacter();
                change.x = Mathf.Round(change.x);
                change.y = Mathf.Round(change.y); // round the X and Y position so it can only be 1 or 0
                animator.SetFloat("moveX", change.x); // influence the animator
                animator.SetFloat("moveY", change.y);

            }
            else
            {
                yield return null;
            }
            animator.SetBool("Jumping", false);

            //PlayerPhysics.enabled = true;
            //Hitbox.enabled = true;
            //speed = speed / 4;
            CurrentState = Playerstate.idle;
        
        
        
    }

    Vector3 ChooseArrowDirection() 
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX"))* Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
        // direction of the arrow depending on your movex and moveY, this is completed with the arrow script

    }
    public void RaiseItem()
    {
        if (OwnInventory.CurrentItem != null)
        {
            if (CurrentState != Playerstate.interact)
            {
                CurrentState = Playerstate.interact;
                //add wait time waitCo()
                animator.SetBool("ItemReceive", true);
                ReceivedItemSprite.sprite = OwnInventory.CurrentItem.ItemSprite;
            }
            else
            {
                animator.SetBool("ItemReceive", false);
                CurrentState = Playerstate.idle;
                ReceivedItemSprite.sprite = null;
                OwnInventory.CurrentItem = null;


            }
        }
    }
    void UpdateAnimationAndMove()
    {
             if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y); // round the X and Y position so it can only be 1 or 0
            animator.SetFloat("moveX", change.x); // influence the animator
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }else
        {
            animator.SetBool("moving", false);


        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime); //physically move
    }

    public void Knock(float KnockTime, float Damage)
    {
        CurrentHealth.RunTimeValue -= Damage;
        PlayerHealthSignal.Raise();
        if (CurrentHealth.RunTimeValue > 0)
        {
            StartCoroutine(KnockCo(KnockTime));
        } else
        {
            this.gameObject.SetActive(false);
        }

    }
    private IEnumerator KnockCo(float KnockTime)
    {
        PlayerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(KnockTime);
            myRigidbody.velocity = Vector2.zero;
            CurrentState = Playerstate.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        TriggerCollider.enabled = false;
        while (temp < FlashQuantity)
        {
            MySprite.color = FlashColor;
            yield return new WaitForSeconds(Flashduration);
            MySprite.color = DefColor;
            yield return new WaitForSeconds(Flashduration);
            temp++;
        }
        TriggerCollider.enabled = true;
    }


    public IEnumerator LiftShield()
    {
        if (animator.GetBool("Blocking") == true)
        {
            CanLiftShield = false;
            yield return new WaitForSeconds(3f);
            CanLiftShield = true;
        }
    }
}
