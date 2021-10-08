using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBullet : Projectile
{
    private bool IsTouching;
    public Transform GroundCheck;
    public float CheckRadius;
    public LayerMask WhatIsLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsTouching = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsLayer);

        if (IsTouching)
        {
            Destroy(this.gameObject);
        }
    }
}
