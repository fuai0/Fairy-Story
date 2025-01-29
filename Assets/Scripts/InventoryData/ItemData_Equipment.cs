using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Special,
    Consumables
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Majior stats")]
    public int strength;
    public int agility;
    public int vitality;

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

    private int descriptionLength;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.vitality.AddModifier(vitality);

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

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.vitality.RemoveModifier(vitality);

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
        descriptionLength = 0;

        AddItemDescription(strength, "strength");
        AddItemDescription(agility, "agility");
        AddItemDescription(vitality, "vitality");

        AddItemDescription(damage, "damage");
        AddItemDescription(cirtChance, "cirtChance");
        AddItemDescription(cirtPower, "cirtPower");

        AddItemDescription(maxHealth, "maxHealth");
        AddItemDescription(armor, "armor");
        AddItemDescription(evasion, "evasion");

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

            descriptionLength++;
        }
    }
}
