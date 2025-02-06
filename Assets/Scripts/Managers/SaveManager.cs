using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public string saveFolder = "Saves";   // 存档文件夹名称
    public string saveFilePrefix = "save";// 存档文件名前缀

    public SaveSlot[] saveSlots;         // 所有存档槽信息

    private int maxSaveSlots = 5;
    private string saveDirectory;
    private SaveData saveData = new SaveData();
    private List<ISaveManager> saveManagers;
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
        saveManagers = FindAllSaveManagers();

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref saveData);
        }

        string fullPath = GetSaveFilePath(slotIndex);
        fileDataHandler.Save(fullPath, saveData);

        LoadSaveSlotsInfo();
    }

    // 从指定槽位加载
    public void LoadGame(int slotIndex)
    {
        string fullPlth = GetSaveFilePath(slotIndex);
        if (!File.Exists(fullPlth)) return;

        saveData = fileDataHandler.Load(fullPlth);

        SceneManager.LoadScene(saveData.sceneIndex); // 优先加载场景

        StartCoroutine(DelayLoad());
    }

    private IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(.1f);
        saveManagers = FindAllSaveManagers();

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(saveData);
        }
    }

    // 获取存档文件路径
    private string GetSaveFilePath(int slotIndex) => Path.Combine(saveDirectory, $"{saveFilePrefix}_{slotIndex}.dat");

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
    public bool IsSaveSlotEmpty(int slotIndex) => saveSlots[slotIndex].isEmpty;

    // 获取每个需要保存数据存档接口
    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }
}