using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory instance;

    public List<ItemData> startingEquipment;

    public List<InventoryItem> equip;
    public Dictionary<ItemData_Equipment, InventoryItem> equipDictionary;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData, InventoryItem> equipmentDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform equipSlotParent;
    [SerializeField] private Transform statSlotParent;

    [Header("Data base")]
    public List<ItemData> itemDataBase;
    public List<InventoryItem> loadedItems;
    public List<ItemData_Equipment> loadedEquips;

    private ItemSlot_UI[] stashItemSlot;
    private ItemSlot_UI[] equipmentItemSlot;
    private EquipSlot_UI[] equipSlot;
    private StatSlot_UI[] statSlot;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equip = new List<InventoryItem>();
        equipDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        stashItemSlot = stashSlotParent.GetComponentsInChildren<ItemSlot_UI>();
        equipmentItemSlot = equipmentSlotParent.GetComponentsInChildren<ItemSlot_UI>();
        equipSlot = equipSlotParent.GetComponentsInChildren<EquipSlot_UI>();
        statSlot = statSlotParent.GetComponentsInChildren<StatSlot_UI>();

        FillUpItemDataBase();
    }

    private void AddLoadItems()
    {
        if (loadedItems.Count > 0 || loadedEquips.Count > 0)
        {
            foreach (var item in loadedItems)
            {
                for (int i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.data);
                }
            }

            foreach (var item in loadedEquips)
            {
                EquipItem(item);
            }
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment oldEquipment = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }

        equip.Add(newItem);
        equipDictionary.Add(newEquipment, newItem);

        newEquipment.AddModifiers();

        RemoveItem(_item);
    }

    public void UnequipItem(ItemData_Equipment itemToRemove)
    {
        if (equipDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equip.Remove(value);
            equipDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment)
            AddToEquipment(_item);
        else if (_item.itemType == ItemType.Material)
            AddToStash(_item);

        UpdateSlotUI();
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    private void AddToEquipment(ItemData _item)
    {
        if (equipmentDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            equipment.Add(newItem);
            equipmentDictionary.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if (equipmentDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                equipment.Remove(value);
                equipmentDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }

        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipDictionary)
            {
                if (item.Key.equipmentType == equipSlot[i].slotType)
                    equipSlot[i].UpdateSlot(item.Value);
            }
        }

        for (int i = 0; i < equipmentItemSlot.Length; i++)
        {
            equipmentItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < equipment.Count; i++)
        {
            equipmentItemSlot[i].UpdateSlot(equipment[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }

        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValue();
        }
    }

    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials)
    {
        List<InventoryItem> materialsToRemove = new List<InventoryItem>();

        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            if (stashDictionary.TryGetValue(_requiredMaterials[i].data, out InventoryItem stashValue))
            {
                if (stashValue.stackSize < _requiredMaterials[i].stackSize)
                {
                    Debug.Log("材料不足");
                    return false;
                }
                else
                {
                    materialsToRemove.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("材料不足");
                return false;
            }
        }

        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            for (int j = 0; j < _requiredMaterials[i].stackSize; j++)
            {
                RemoveItem(_requiredMaterials[i].data);
            }
        }

        AddItem(_itemToCraft);

        Debug.Log(_itemToCraft.itemName + " 制作成功");
        return true;
    }

    public List<InventoryItem> GetEquipmentList() => equip;
    public List<InventoryItem> GetStashList() => stash;

    public ItemData_Equipment GetEquip(EquipmentType _type)
    {
        ItemData_Equipment equipedItemData = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipDictionary)
        {
            if (item.Key.equipmentType == _type)
                equipedItemData = item.Key;
        }

        return equipedItemData;
    }

    public void LoadData(SaveData _data)
    {
        foreach (var pair in _data.inventoryId)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemId == pair.Key)
                {
                    InventoryItem itemLoad = new InventoryItem(item);
                    itemLoad.stackSize = pair.Value;

                    loadedItems.Add(itemLoad);
                }
            }
        }

        foreach (var pair in _data.equipId)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemId == pair)
                {
                    loadedEquips.Add(item as ItemData_Equipment);
                }
            }
        }
        AddLoadItems();
    }

    public void SaveData(ref SaveData _data)
    {
        _data.inventoryId.Clear();
        _data.equipId.Clear();

        foreach (var pair in equipmentDictionary)
        {
            _data.inventoryId.Add(pair.Key.itemId, pair.Value.stackSize);
        }

        foreach (var pair in stashDictionary)
        {
            _data.inventoryId.Add(pair.Key.itemId, pair.Value.stackSize); 
        }

        foreach (var pair in equipDictionary)
        {
            _data.equipId.Add(pair.Key.itemId);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("fill up item data base")]
    private void FillUpItemDataBase() => itemDataBase = new List<ItemData>(GetItemDataBase());

    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });

        foreach (var SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }

        return itemDataBase;
    }
#endif
}
