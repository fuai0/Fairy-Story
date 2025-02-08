using UnityEngine;
using UnityEngine.UI;

public class Consumable_UI : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Update()
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
}
