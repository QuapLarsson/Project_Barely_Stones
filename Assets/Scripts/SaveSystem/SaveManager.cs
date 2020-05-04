using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager 
{
    [HideInInspector]
    private static BinaryFormatter formatter;
    
    public static void SaveData(SavePoint _savePoint, int _saveSlot)
    {
        string path = Application.persistentDataPath + "/SaveData/SaveSlot" + _saveSlot + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData saveData = new SaveData(_savePoint);
        formatter = new BinaryFormatter();
        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData LoadData(int _saveSlot)
    {
        string path = Application.persistentDataPath + "/SaveData/SaveSlot" + _saveSlot + ".bin";
        if (File.Exists(path))
        {
            formatter = new BinaryFormatter();
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
