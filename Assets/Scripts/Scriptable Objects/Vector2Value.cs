using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Vector2Value : ScriptableObject
{
    [Header("Value Running in game")]
    public Vector2 InitialValue;

    [Header("Value By Default")]
    public Vector2 DefaultValue;
    

}
