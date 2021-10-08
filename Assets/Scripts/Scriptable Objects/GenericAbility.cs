using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability")]
public class GenericAbility : ScriptableObject
{
    public virtual void Ability(Vector2 PlayerPosition, Vector2 PlayerDirection , Animator PAnim = null, Rigidbody2D Rb = null)
    {

    }
}
