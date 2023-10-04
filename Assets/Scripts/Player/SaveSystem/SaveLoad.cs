using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveLoad
{
    private static string directory = "/SaveData/";
    private static string fileName = "SaveGame";
    private static string fileExtension = ".save";

    public static bool Save(SaveData data, string saveID)
    {
        EventBus.Publish(EventType.PLAYER_SAVEGAME);

        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Debug.Log("Creating dictionary");
            Directory.CreateDirectory(dir);
        }

        string Json = JsonUtility.ToJson(data, prettyPrint:true);
        File.WriteAllText(path:dir + fileName + saveID + fileExtension, contents:Json);
        Debug.Log(dir);
        Debug.Log("Saving Game");

        return true;
    }

    public static SaveData Load(string saveID)
    {
        string fullPath = Application.persistentDataPath + directory + fileName + saveID + fileExtension;
        SaveData data = new SaveData();
        

        if (File.Exists(fullPath)) 
        { 
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<SaveData>(json);

            //EventBus.Publish(EventType.PLAYER_LOADGAME);

            Debug.Log("Load");
        }
        else
        {
            Debug.Log("Save file does not exist");
        }

        return data;
    }

    public static void DeleteSaveData(string saveID)
    {
        string fullPath = Application.persistentDataPath + directory + fileName + saveID + fileExtension;

        if (!File.Exists(fullPath)) 
        {
            File.Delete(fullPath);
        }
    }
}
