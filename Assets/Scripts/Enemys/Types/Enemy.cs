using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger,
    peace
}
public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState CurrentState;

    [Header("Stats")]
    public float Health;
    public FloatValue MaxHealth;
    public float WalkSpeed;
    public string ItsName;
    public int BaseAttack;
    public Vector2 HomePosition;
    public BoxCollider2D TriggerCollider;
    public EnumValues State;

    [Header("Death Effects")]
    public GameObject Deatheffect;
    public LootTable ThisLoot;
    public SignalSender DeathSignal;
    public GeneralKarmaManager GeneralKarmaManager;

    [Header("Death Signals")]
    public SignalSender RoomSignal;

    [Header("Pacifist Attributes")]
    [SerializeField] public int MonsterKarma;
    [SerializeField] public int MaxKarma;
    [SerializeField] public bool spared ;

    [Header("Iced")]
    public Color IcedColor;
    public Color DefColor;
    public float IcedDuration;




    private void Awake()
    {
        Health = MaxHealth.InitialValue;
        MonsterKarma = 0;
    }

    private void OnEnable()
    {
        transform.position = HomePosition;
        Health = MaxHealth.InitialValue;
        CurrentState = EnemyState.idle;
    }
    private void TakeDamage (float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            if (RoomSignal != null)
            {
                RoomSignal.Raise();
            }
            DeathEffect();
            MakeLoot();
            GeneralKarmaManager.AddNameToList(ItsName);
            DeathSignal.Raise();
            State.CurrentState = States.Dead;
            this.gameObject.SetActive (false);
        }
    }

    private void MakeLoot()
    {
        if(ThisLoot != null)
        {
            PowerUp current = ThisLoot.LootPowerUp();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
    private void DeathEffect()
    {
        if(Deatheffect != null)
        {
            float DeathDelay = 1f;
            GameObject effect = Instantiate(Deatheffect, transform.position, Quaternion.identity);
            Destroy(effect, DeathDelay);
        }
    }
    public void Knock(Rigidbody2D myRigB, float KnockTime, float Damage )
    {
        StartCoroutine(KnockCo(myRigB, KnockTime));
        TakeDamage(Damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigB, float KnockTime)
    {
        if (myRigB != null )
        {
            yield return new WaitForSeconds(KnockTime);
            myRigB.velocity = Vector2.zero;
            CurrentState = EnemyState.idle;
            myRigB.velocity = Vector2.zero;
        }
    }
}
