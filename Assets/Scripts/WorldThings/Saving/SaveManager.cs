using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Gamesave; // It doesn't change when loaded
    public List<ScriptableObject> Objects = new List<ScriptableObject>();
   
    [Header("Data Management")]
    public FileStream DefaultFile;
    public FileStream SavedFile;
    // to restart the default you game to do it manually and playing the game once and deleting all /

    public void OnEnable()
    {
       // SetDefault();
    }
    public void ResetScriptables()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath +
                string.Format("/{0}.dat", i));
            }
        }
    }


    public void SaveScriptables()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            SavedFile = File.Create(Application.persistentDataPath +
                string.Format("/n{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var JSON = JsonUtility.ToJson(Objects[i]);
            binary.Serialize (SavedFile, JSON);
            SavedFile.Close();
        }
    }


    public void SaveDefault()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            DefaultFile = File.Create(Application.persistentDataPath +
                string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var JSON = JsonUtility.ToJson(Objects[i]);
            binary.Serialize(DefaultFile, JSON);
            DefaultFile.Close();
        }
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath +
                string.Format("/n{0}.dat", i)))
            {
                SavedFile = File.Open(Application.persistentDataPath +
                string.Format("/n{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(SavedFile), Objects[i]);
                SavedFile.Close();
            }
        }


    } 
    public void LoadDefault()
    {
        for (int i = 0; i<Objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                DefaultFile = File.Open(Application.persistentDataPath +
                string.Format("/{0}.dat", i), FileMode.Open);
    BinaryFormatter binary = new BinaryFormatter();
    JsonUtility.FromJsonOverwrite((string) binary.Deserialize(DefaultFile), Objects[i]);
                DefaultFile.Close();

            }
        }


    }
     
}

