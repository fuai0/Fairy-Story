using System.IO;
using TMPro;
using UnityEngine;

public class SaveSlot_UI : MonoBehaviour
{
    private TextMeshProUGUI slotName;
    [SerializeField] private int slotIndex;

    private void Start()
    {
        slotName = GetComponentInChildren<TextMeshProUGUI>();

        Initialize();
    }
 
    public void Initialize()
    {
        SaveSlot[] slotData = SaveManager.instance.saveSlots;

        if (slotData[slotIndex].isEmpty)
        {
            slotName.text = "¿Õ´æµµ";
        }
        else
        {
            slotName.text = "´æµµ_" + slotIndex.ToString();
        }
    }

    public void SaveSlot()
    {
        SaveManager.instance.SaveGame(slotIndex);
        Initialize();
    }
    public void LoadSlot()
    {
        SaveManager.instance.LoadGame(slotIndex);
        Initialize();
    }
}
