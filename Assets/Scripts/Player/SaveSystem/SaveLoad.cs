using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveLoad
{
    private static string directory = "/SaveData/";
    private static string fileName = "SaveGame.save";

    public static bool Save(SaveData data)
    {
        EventBus.Publish(EventType.PLAYER_SAVEGAME);

        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Debug.Log("Creating dictionary");
            Directory.CreateDirectory(dir);
        }

        string Json = JsonUtility.ToJson(data, prettyPrint:true);
        File.WriteAllText(path:dir + fileName, contents:Json);
        Debug.Log(dir);
        Debug.Log("Saving Game");

        return true;
    }

    public static SaveData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
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

    public static void DeleteSaveData()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;

        if (!File.Exists(fullPath)) 
        {
            File.Delete(fullPath);
        }
    }
}
