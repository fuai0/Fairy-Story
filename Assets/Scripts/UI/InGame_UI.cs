using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame_UI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI healthText;
    private CharacterStats stats;
    private Entity entity;

    private void Start()
    {
        stats = GetComponentInParent<CharacterStats>();
        entity = GetComponentInParent<Entity>();

        UpdateHealthUI();
    }

    private void Update()
    {
        UpdateComsumableUI();
        UpdateHealthUI();
    }

    private void UpdateComsumableUI()
    {
        ItemData_Equipment consumable = Inventory.instance.GetEquip(EquipmentType.Consumable);
        if (consumable != null)
        {
            image.color = Color.white;
            image.sprite = consumable.icon;
        }
        else
            image.color = Color.clear;
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetHealth();
        slider.value = stats.currentHealth;
        healthText.text = slider.value.ToString() + " / " + slider.maxValue.ToString();
    }
}
