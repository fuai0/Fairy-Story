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

    public SerializableDictionary<string, bool> checkpoints;
    public string closestCheckpointId;

    public SerializableDictionary<string, float> volumeSettings;

    public SaveData()
    {
        inventoryId = new SerializableDictionary<string, int>();
        equipId = new List<string>();

        closestCheckpointId = string.Empty;
        checkpoints = new SerializableDictionary<string, bool>();
        volumeSettings = new SerializableDictionary<string, float>();
    }
}
