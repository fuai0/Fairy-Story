using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public string saveFolder = "Saves";   // �浵�ļ�������
    public string saveFilePrefix = "save";// �浵�ļ���ǰ׺

    public SaveSlot[] saveSlots;         // ���д浵����Ϣ

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

    // ��ʼ���浵ϵͳ
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

    // �������д浵����Ϣ
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

    // ���浽ָ����λ
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

    // ��ָ����λ����
    public void LoadGame(int slotIndex)
    {
        string fullPlth = GetSaveFilePath(slotIndex);
        if (!File.Exists(fullPlth)) return;

        saveData = fileDataHandler.Load(fullPlth);

        SceneManager.LoadScene(saveData.sceneIndex); // ���ȼ��س���

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

    // ��ȡ�浵�ļ�·��
    private string GetSaveFilePath(int slotIndex) => Path.Combine(saveDirectory, $"{saveFilePrefix}_{slotIndex}.dat");

    // ɾ���浵
    public void DeleteSave(int slotIndex)
    {
        string path = GetSaveFilePath(slotIndex);
        if (File.Exists(path))
        {
            File.Delete(path);
            saveSlots[slotIndex].isEmpty = true;
        }
    }

    // ���浵�Ƿ����
    public bool IsSaveSlotEmpty(int slotIndex) => saveSlots[slotIndex].isEmpty;

    // ��ȡÿ����Ҫ�������ݴ浵�ӿ�
    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }
}