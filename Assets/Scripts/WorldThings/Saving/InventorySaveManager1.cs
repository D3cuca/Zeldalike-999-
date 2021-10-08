using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using Leguar.TotalJSON;

[System.Serializable]
public class InventorySaveManager1 : MonoBehaviour
{
    public static SaveManager Gamesave;

    [SerializeField] private PlayerInventory PlayerInventory;
    public FileStream DefaultFile;
    public FileStream SavedFile;

    private SerializableListStrings SL = new SerializableListStrings();
    public InventoryDataBase ItemDB;
    



    public void SaveData()
    {
        SL.SerializableList.Clear();
        BuildSaveData();
        SaveScriptable();
    }

    public void LoadData()
    {
        PlayerInventory.MyInventory.Clear();
        Debug.Log("Inventory Count = " + PlayerInventory.MyInventory.Count);

        SL.SerializableList.Clear();

        LoadSavedData();

        ImportSaveData();
        PlayerInventory.PutImages();

    }


    public void SaveDefaultData()
    {
        SL.SerializableList.Clear();
        BuildDefaultSaveData();
        SaveDefaultScriptable();
    }

    public void LoadDefaultData()
    {
        PlayerInventory.MyInventory.Clear();
        Debug.Log("Inventory Count = " + PlayerInventory.MyInventory.Count);

        SL.SerializableList.Clear();

        LoadDefaultSavedData();

        ImportDefaultSaveData();

        PlayerInventory.PutImages();

    }


    private void BuildSaveData()
    {
        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
        {
            SerializableListStrings.SerialItem SI = new SerializableListStrings.SerialItem();
            SI.ItemName = PlayerInventory.MyInventory[i].ItemName;
            SI.Count = PlayerInventory.MyInventory[i].NumberHeld;
            
            SL.SerializableList.Add(SI);
        }
    }

    public void SaveScriptable()
    {
        Debug.Log("IS: Saving to: " + Application.persistentDataPath);

        string FilePath = Application.persistentDataPath + "/newSave.json";

        StreamWriter STW = new StreamWriter(FilePath);

        JSON jsonObject = JSON.Serialize(SL);

        string json = jsonObject.CreatePrettyString();

        STW.WriteLine(json);

        STW.Close();
    }



    private void LoadSavedData()
    {
        Debug.Log("IS: Loading From: " + Application.persistentDataPath);

        string FilePath = Application.persistentDataPath + "/newSave.json";

        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);

            JSON jsonObject = JSON.ParseString(json);

            SL = jsonObject.Deserialize<SerializableListStrings>();
        }
    }


    private void ImportSaveData()
    {
        for(int i = 0; i < SL.SerializableList.Count; i++)
        {
            string name = SL.SerializableList[i].ItemName;
            int count = SL.SerializableList[i].Count;

            InventoryItem obj = ItemDB.GetItem(name);

            if (obj)
            {
                obj.NumberHeld = count;

                PlayerInventory.MyInventory.Add(obj);
                Debug.Log("Added " + obj.name + "Count" + obj.NumberHeld + "to inventory");
            }
            else
            {
                Debug.LogError("Item DB not found :" + SL.SerializableList[i].ItemName);

            }

        }
    }








    private void BuildDefaultSaveData()
    {
        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
        {
            SerializableListStrings.SerialItem SI = new SerializableListStrings.SerialItem();
            SI.ItemName = PlayerInventory.MyInventory[i].ItemName;
            SI.Count = PlayerInventory.MyInventory[i].NumberHeld;

            SL.SerializableList.Add(SI);
        }
    }

    public void SaveDefaultScriptable()
    {
        Debug.Log("Default: Saving to: " + Application.persistentDataPath);

        string FilePath = Application.persistentDataPath + "/DefaultSave.json";

        StreamWriter STW = new StreamWriter(FilePath);

        JSON jsonObject = JSON.Serialize(SL);

        string json = jsonObject.CreatePrettyString();

        STW.WriteLine(json);

        STW.Close();
    }



    private void LoadDefaultSavedData()
    {
        Debug.Log("IS: Loading From: " + Application.persistentDataPath);

        string FilePath = Application.persistentDataPath + "/DefaultSave.json";

        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);

            JSON jsonObject = JSON.ParseString(json);

            SL = jsonObject.Deserialize<SerializableListStrings>();
        }
    }


    private void ImportDefaultSaveData()
    {
        for (int i = 0; i < SL.SerializableList.Count; i++)
        {
            string name = SL.SerializableList[i].ItemName;
            int count = SL.SerializableList[i].Count;

            InventoryItem obj = ItemDB.GetItem(name);

            if (obj)
            {
                obj.NumberHeld = count;

                PlayerInventory.MyInventory.Add(obj);
                Debug.Log("Added " + obj.name + "Count" + obj.NumberHeld + "to inventory");
            }
            else
            {
                Debug.LogError("Item DB not found :" + SL.SerializableList[i].ItemName);

            }

        }
    }
}
