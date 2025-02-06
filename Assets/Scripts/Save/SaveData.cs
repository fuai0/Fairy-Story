using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int sceneIndex;        
    public Vector2 playerPosition; 

    public SerializableDictionary<string, int> inventoryId;
    public List<string> equipId;

    public SerializableDictionary<string, bool> checkPoints;
    public string closestCheckPointId;

    public SerializableDictionary<string, float> volumeSettings;

    public SaveData()
    {
        inventoryId = new SerializableDictionary<string, int>();
        equipId = new List<string>();

        closestCheckPointId = string.Empty;
        checkPoints = new SerializableDictionary<string, bool>();
        volumeSettings = new SerializableDictionary<string, float>();
    }
}
