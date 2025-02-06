using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatSlot_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private StatType statType;

    private void Start()
    {
        UpdateStatValue();
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