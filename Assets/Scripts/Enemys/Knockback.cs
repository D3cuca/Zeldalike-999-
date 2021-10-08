using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float Thrust;
    public float KnockTime;
    public float Damage;


    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Others") || other.gameObject.CompareTag ("Player") && other.isTrigger)
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * Thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("Others") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().CurrentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, KnockTime, Damage);
                }
              
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovement>().CurrentState != Playerstate.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().CurrentState = Playerstate.stagger;
                        other.GetComponent<PlayerMovement>().Knock(KnockTime, Damage);
                    }
                }


            }
        }
    }


}
