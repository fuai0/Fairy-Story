using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftList_UI : MonoBehaviour
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private GameObject craftSlotPrefab;

    [SerializeField] private List<ItemData_Equipment> craftEquipment;

    void Start()
    {
        transform.parent.GetChild(0).GetComponent<CraftList_UI>().SetupCraftList();
    }


    private void SetupCraftList()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        {
            Destroy(craftSlotParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < craftEquipment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftSlotPrefab, craftSlotParent);
            newSlot.GetComponent<CraftSlot_UI>().SetupCraftSlot(craftEquipment[i]);
        }
    }

    public void ChangeCraft()
    {
        SetupCraftList();
    }

    public void SetupDefaultCraftWindow()
    {
        if (craftEquipment[0] != null)
            GetComponentInParent<UI>().craftWindow.SetupCraftWindow(craftEquipment[0]);
    }
}
