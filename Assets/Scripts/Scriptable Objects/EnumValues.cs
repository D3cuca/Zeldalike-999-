using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Enemy, 
    Friend,
    Dead
}

[CreateAssetMenu]
[System.Serializable]

public class EnumValues : ScriptableObject
{
    public States CurrentState;
}