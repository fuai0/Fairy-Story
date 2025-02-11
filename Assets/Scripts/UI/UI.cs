using System.Collections;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject craftTip;
    [SerializeField] private GameObject worldUI;
    [SerializeField] private GameObject checkMenuUI;
    [SerializeField] private GameObject deadUI;


    [SerializeField] private GameObject majorStatsUI;
    [SerializeField] private GameObject statsUI;

    [Header("Item Tip")]
    [SerializeField] private GameObject itemToolTip;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public CraftWindow_UI craftWindow;
    public static UI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Tab))
            SwitchWithKeyTo(characterUI);

        if (Input.GetKeyDown(KeyCode.I))
            SwitchWithKeyTo(inventoryUI);

        if (Input.GetKeyDown(KeyCode.M))
            SwitchWithKeyTo(mapUI);

        if (Input.GetKeyDown(KeyCode.O))
            SwitchWithKeyTo(optionUI);
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        _menu.SetActive(true);
        GameManager.instance.PauseGame(true);
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            GameManager.instance.PauseGame(false);
            return;
        }

        SwitchTo(_menu);
    }

    public void SwitchStatsUI(GameObject _statsUI)
    {
        majorStatsUI.SetActive(false);
        statsUI.SetActive(false);

        _statsUI.SetActive(true);
    }

    public void SwitchCraftUI() => SwitchWithKeyTo(craftUI);

    public void SwitchWorldUI() => SwitchWithKeyTo(worldUI);

    public void SwitchCheckMenuUI() => SwitchWithKeyTo(checkMenuUI);

    public void ResetTime()
    {
        GameManager.instance.PauseGame(false);
    }

    public void ShowItemTip(ItemData _itemData)
    {
        itemNameText.text = _itemData.itemName;
        itemTypeText.text = "材料";
        itemDescription.text = _itemData.itemDescription;

        itemToolTip.SetActive(true);
    }

    public void ShowEquipmentTip(ItemData_Equipment _itemData)
    {
        itemNameText.text = _itemData.itemName;

        switch(_itemData.equipmentType)
        {
            case EquipmentType.Weapon: itemTypeText.text = "武器"; break;
            case EquipmentType.Armor: itemTypeText.text = "护甲"; break;
            case EquipmentType.Special: itemTypeText.text = "特殊物品"; break;
            case EquipmentType.Consumable: itemTypeText.text = "消耗品"; break;
        }

        itemDescription.text = _itemData.GetDescription();


        itemToolTip.SetActive(true);
    }

    public void HideItemTip()
    {
        itemToolTip.SetActive(false);
    }

    public void ShowDeadUI() => deadUI.SetActive(true);
    public void HideDeadUI() => deadUI.SetActive(false);

    public void Restart() => GameManager.instance.Restart();

    public void ShowCraftTip(string _text)
    {
        craftTip.SetActive(true);
        craftTip.GetComponentInChildren<TextMeshProUGUI>().text = _text;
    }

    public void HideCraftTip()
    {
        StartCoroutine(DelayHide());
    }

    private IEnumerator DelayHide()
    {
        yield return new WaitForSeconds(2f);

        craftTip.SetActive(false);
    }
}

