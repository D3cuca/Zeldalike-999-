using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualKarmaManager : MonoBehaviour
{
    [Header("Enemy Stuff")]
    [SerializeField] private MonoBehaviour EnemyAttributes;


    [Header("Friend Stuff")]
    [SerializeField] private MonoBehaviour FriendAttributes;


    [Header("Variabes")]
    public int Karma;
    public int KarmaMax;
    
    // Start is called before the first frame update
    void Start()
    {

        KarmaMax = EnemyAttributes.GetComponent<Enemy>().MaxKarma;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyAttributes.isActiveAndEnabled == true)
        {
            Karma = EnemyAttributes.GetComponent<Enemy>().MonsterKarma;
            if (Karma >= KarmaMax)
            {
                EnemyAttributes.enabled = false;
                FriendAttributes.enabled = true;
                this.gameObject.tag = "Untagged";
                EnemyAttributes.GetComponent<Enemy>().State.CurrentState = States.Friend;
            }
            
        }
    }
}
