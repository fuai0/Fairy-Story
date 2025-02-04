using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Overlays;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Header("存档配置")]
    public int maxSaveSlots = 5;          // 最大存档槽数量
    public string saveFolder = "Saves";   // 存档文件夹名称
    public string saveFilePrefix = "save";// 存档文件名前缀

    public SaveSlot[] saveSlots;         // 所有存档槽信息
    private string saveDirectory;         // 存档完整路径

    private FileDataHandler fileDataHandler;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSaveSystem();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fileDataHandler = new FileDataHandler();    
    }

    // 初始化存档系统
    private void InitializeSaveSystem()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, saveFolder);
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        saveSlots = new SaveSlot[maxSaveSlots];
        LoadSaveSlotsInfo();
    }

    // 加载所有存档槽信息
    private void LoadSaveSlotsInfo()
    {
        for (int i = 0; i < maxSaveSlots; i++)
        {
            string filePath = GetSaveFilePath(i);
            saveSlots[i] = new SaveSlot
            {
                savePath = filePath,
                isEmpty = !File.Exists(filePath)
            };
        }
    }

    // 保存到指定槽位
    public void SaveGame(int slotIndex)
    {
        SaveData data = new SaveData
        {
            saveIndex = slotIndex,
            saveName = "存档_" + DateTime.Now.ToString("yyyyMMdd_HHmm"),
            sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex,
            playerPosition = PlayerManager.instance.player.transform.position,
        };

        string fullPath = GetSaveFilePath(slotIndex);
        fileDataHandler.Save(fullPath, data);
        LoadSaveSlotsInfo();
    }

    // 获取存档文件路径
    private string GetSaveFilePath(int slotIndex)
    {
        return Path.Combine(saveDirectory, $"{saveFilePrefix}_{slotIndex}.dat");
    }

    // 从指定槽位加载
    public void LoadGame(int slotIndex)
    {
        string fullPlth = GetSaveFilePath(slotIndex);
        if (!File.Exists(fullPlth)) return;

        fileDataHandler.Load(fullPlth);
    }

    // 删除存档
    public void DeleteSave(int slotIndex)
    {
        string path = GetSaveFilePath(slotIndex);
        if (File.Exists(path))
        {
            File.Delete(path);
            saveSlots[slotIndex].isEmpty = true;
        }
    }

    // 检查存档是否存在
    public bool IsSaveSlotEmpty(int slotIndex)
    {
        return saveSlots[slotIndex].isEmpty;
    }
}