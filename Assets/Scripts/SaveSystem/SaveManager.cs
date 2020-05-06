using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager 
{
    [HideInInspector]
    public static bool isSaving;
    
    public static void SavePlayerData(SavePoint _savePoint, int _saveSlot)
    {
        isSaving = true;
        string path = Application.persistentDataPath + "/SaveSlot" + _saveSlot + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(_savePoint);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, saveData);

        stream.Close();
        isSaving = false;
    }

    public static SaveData LoadData(int _saveSlot)
    {
        string path = Application.persistentDataPath + "/SaveSlot" + _saveSlot + ".bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData loadData = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return loadData;
        }
        else
        {
            return null;
        }
    }
}
