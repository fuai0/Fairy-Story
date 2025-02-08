using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effext", menuName = "Data/Item Effect/Heal Effext")]

public class Heal_Effect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPrecent;

    public override void ExecuteEffect(EnemyStats _enemyStats, PlayerStats _playerStats)
    {
        int healAmount = Mathf.RoundToInt(_playerStats.maxHealth.GetValue() * healPrecent);

        _playerStats.IncreaseHealth(healAmount);
    }
}

