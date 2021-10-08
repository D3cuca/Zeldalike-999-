using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InventorySaveManager : MonoBehaviour
{
    public static SaveManager Gamesave;

    [SerializeField] private PlayerInventory PlayerInventory;
    public FileStream DefaultFile;
    public FileStream SavedFile;



    private void OnEnable()
    {



    }

    public void ResetScriptables()
    {
        int i = 0;
        while(File.Exists(Application.persistentDataPath +
                string.Format("/{0}.inv", i)))
            { 
                File.Delete(Application.persistentDataPath +
                string.Format("/{0}.inv", i));
            i++;
            Debug.Log("Reseting");
            }
        
    }
    public void ResetScriptables2()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath +
                string.Format("/n{0}.inv", i)))
        {
            File.Delete(Application.persistentDataPath +
            string.Format("/n{0}.inv", i));
            i++;
            Debug.Log("Reseting");
        }

    }


    public void SaveScriptables()
    { 

        ResetScriptables2();
        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
        {
            SavedFile = File.Create(Application.persistentDataPath +
                string.Format("/n{0}.inv", i));
            BinaryFormatter binary = new BinaryFormatter();
            var JSON = JsonUtility.ToJson(PlayerInventory.MyInventory[i]);
            binary.Serialize(SavedFile, JSON);
            SavedFile.Close();
             Debug.Log("N is saved");
        }
    }


    public void SaveDefault()
    {
        Debug.Log("Save null is read");
        ResetScriptables();
        for (int i = 0; i < PlayerInventory.MyInventory.Count; i++)
        {
            DefaultFile = File.Create(Application.persistentDataPath +
                string.Format("/{0}.inv", i));
            BinaryFormatter binary = new BinaryFormatter();
            var JSON = JsonUtility.ToJson(PlayerInventory.MyInventory[i]);
            binary.Serialize(DefaultFile, JSON);
            DefaultFile.Close();
            Debug.Log("Null is saved");
        }
    }

    public void LoadScriptables()
    {

        PlayerInventory.MyInventory.Clear();
        int i = 0;
        while (File.Exists(Application.persistentDataPath +
                string.Format("/n{0}.inv", i)))
        {
                var temp = ScriptableObject.CreateInstance<InventoryItem>();
                SavedFile = File.Open(Application.persistentDataPath +
                string.Format("/n{0}.inv", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(SavedFile), temp);
                PlayerInventory.MyInventory.Add(temp);
                SavedFile.Close();
                PlayerInventory.MyInventory.Add(temp);
                i++;

                Debug.Log("N is loaded");  
        }
        


    }
    public void LoadDefault()
    {

        PlayerInventory.MyInventory.Clear();

        if (PlayerInventory.MyInventory.Contains(null))
        {
            Debug.Log("inventory cleared");
        }
        int i = 0;
        while(File.Exists(Application.persistentDataPath +
                string.Format("/{0}.inv", i))) 
        {
            var temp = ScriptableObject.CreateInstance<InventoryItem>();
                DefaultFile = File.Open(Application.persistentDataPath +
                string.Format("/{0}.inv", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(DefaultFile), temp);
                DefaultFile.Close();
            PlayerInventory.MyInventory.Add(temp);
        Debug.Log("Null is loaded");
            i++;

            }
        


    }
    



}
