using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableListStrings
{
    public struct SerialItem
    {
        public string ItemName;
        public int Count;
    }

    public List<SerialItem> SerializableList = new List<SerialItem>();

}
