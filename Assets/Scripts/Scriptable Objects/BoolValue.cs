﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    [Header("Value Running In Game")]
    public bool InitialValue;

    public bool RuntimeValue;

  
}
