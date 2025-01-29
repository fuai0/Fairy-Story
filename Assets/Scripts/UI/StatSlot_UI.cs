using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private StatType statType;

    [TextArea]
    [SerializeField] private string statDescription;

    private void Start()
    {
        UpdateStatValue();

        statValueText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateStatValue()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            statValueText.text = playerStats.GetStat(statType).GetValue().ToString();
        }

        if (statType == StatType.maxHealth)
            statValueText.text = playerStats.GetHealth().ToString();
        if (statType == StatType.damage)
            statValueText.text = (playerStats.strength.GetValue() + playerStats.damage.GetValue()).ToString();
        if (statType == StatType.cirtPower)
            statValueText.text = (playerStats.cirtPower.GetValue() + playerStats.strength.GetValue()).ToString();
        if (statType == StatType.cirtChance)
            statValueText.text = (playerStats.cirtChance.GetValue() + playerStats.agility.GetValue()).ToString();
        if (statType == StatType.evasion)
            statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();
    }
}