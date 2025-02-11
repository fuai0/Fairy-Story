using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftWindow_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button craftButton;

    [SerializeField] private Image[] materialImage;

    [SerializeField] private GameObject craftTip;
    [SerializeField] private TextMeshProUGUI craftName;
    [SerializeField] private TextMeshProUGUI craftDescription;
    private ItemData_Equipment equipment;

    public void SetupCraftWindow(ItemData_Equipment _data)
    {
        equipment = _data;

        craftButton.onClick.RemoveAllListeners();

        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for (int i = 0; i < _data.craftingMaterials.Count; i++)
        {
            materialImage[i].sprite = _data.craftingMaterials[i].data.icon;
            materialImage[i].color = Color.white;

            TextMeshProUGUI materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();

            materialSlotText.text = _data.craftingMaterials[i].stackSize.ToString();
            materialSlotText.color = Color.white;
        }

        itemIcon.sprite = _data.icon;
        itemIcon.color = Color.white;

        craftButton.onClick.AddListener(() => Inventory.instance.CanCraft(_data, _data.craftingMaterials));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equipment == null)
            return;

        craftName.text = equipment.itemName;
        craftDescription.text = equipment.GetDescription();

        craftTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (equipment == null)
            return;

        craftTip.SetActive(false);
    }
}
