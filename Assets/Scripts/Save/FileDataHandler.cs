using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    public void Save(string fullPath,SaveData _data)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataStore = JsonUtility.ToJson(_data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataStore);
                }
            }
        }

        catch (Exception e)
        {
            Debug.LogError("error on trying to save data to file : " + fullPath + "\n" + e);
        }

    }

    public SaveData Load(string fullPath)
    {
        SaveData loadData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataLoad = reader.ReadToEnd();
                    }
                }
                loadData = JsonUtility.FromJson<SaveData>(dataLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("error on trying to load data to file : " + fullPath + "\n" + e);
            }
        }

        return loadData;
    }

    public void Delete(string fullPath)
    {
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
