using System;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int saveIndex;
    public string saveName;       // 存档名称
    public int sceneIndex;        // 当前场景索引
    public Vector3 playerPosition; // 玩家位置
    // 其他自定义数据...
}
