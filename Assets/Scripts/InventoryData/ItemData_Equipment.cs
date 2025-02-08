using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Special,
    Consumable
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Offensive stats")]
    public int damage;
    public int cirtChance;
    public int cirtPower;

    [Header("Defencive stats")]
    public int maxHealth;
    public int armor;
    public int evasion;

    [Header("craft requirement")]
    public List<InventoryItem> craftingMaterials;

    [Header("Unique effect")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;

    public void Effect(EnemyStats _enemyStats = null, PlayerStats _playerStats = null)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(_enemyStats,_playerStats);
        }
    }

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.damage.AddModifier(damage);
        playerStats.cirtChance.AddModifier(cirtChance);
        playerStats.cirtPower.AddModifier(cirtPower);

        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.damage.RemoveModifier(damage);
        playerStats.cirtChance.RemoveModifier(cirtChance);
        playerStats.cirtPower.RemoveModifier(cirtPower);

        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
    }

    public override string GetDescription()
    {
        sb.Length = 0;

        AddItemDescription(damage, "¹¥»÷ÉËº¦");
        AddItemDescription(cirtChance, "±©»÷ÂÊ");
        AddItemDescription(cirtPower, "±©»÷ÉËº¦");

        AddItemDescription(maxHealth, "×î´óÉúÃüÖµ");
        AddItemDescription(armor, "»¤¼×");
        AddItemDescription(evasion, "ÉÁ±Ü");

        for (int i = 0; i < itemEffects.Length; i++)
        {
            if (itemEffects[i].effectDescription.Length > 0)
            {
                sb.AppendLine();
                sb.Append(itemEffects[i].effectName + " : " + itemEffects[i].effectDescription);
            }
        }

        sb.Append(itemDescription);

        return sb.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (_value > 0)
                sb.Append("+ " + _value + " " + _name);
            if (_value < 0)
                sb.Append("- " + (-_value) + " " + _name);
        }
    }
}
