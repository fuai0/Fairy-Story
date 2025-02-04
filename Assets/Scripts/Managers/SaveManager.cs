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

    [Header("�浵����")]
    public int maxSaveSlots = 5;          // ���浵������
    public string saveFolder = "Saves";   // �浵�ļ�������
    public string saveFilePrefix = "save";// �浵�ļ���ǰ׺

    public SaveSlot[] saveSlots;         // ���д浵����Ϣ
    private string saveDirectory;         // �浵����·��

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
        SaveData data = new SaveData
        {
            saveIndex = slotIndex,
            saveName = "�浵_" + DateTime.Now.ToString("yyyyMMdd_HHmm"),
            sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex,
            playerPosition = PlayerManager.instance.player.transform.position,
        };

        string fullPath = GetSaveFilePath(slotIndex);
        fileDataHandler.Save(fullPath, data);
        LoadSaveSlotsInfo();
    }

    // ��ȡ�浵�ļ�·��
    private string GetSaveFilePath(int slotIndex)
    {
        return Path.Combine(saveDirectory, $"{saveFilePrefix}_{slotIndex}.dat");
    }

    // ��ָ����λ����
    public void LoadGame(int slotIndex)
    {
        string fullPlth = GetSaveFilePath(slotIndex);
        if (!File.Exists(fullPlth)) return;

        fileDataHandler.Load(fullPlth);
    }

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
    public bool IsSaveSlotEmpty(int slotIndex)
    {
        return saveSlots[slotIndex].isEmpty;
    }
}