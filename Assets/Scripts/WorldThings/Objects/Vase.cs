using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    public Animator Anim;
    public LootTable ThisLoot;
    // Start is called before the first frame update
    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            Anim.SetBool("smash", true);
            StartCoroutine(Smash());
        }
    }
    IEnumerator Smash()
    {

        yield return new WaitForSeconds(.4f);
        MakeLoot();
        this.gameObject.SetActive(false);
    }

    private void MakeLoot()
    {
        if (ThisLoot != null)
        {
            PowerUp current = ThisLoot.LootPowerUp();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
