using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effext", menuName = "Data/Item Effect/Blood Effext")]

public class Bleed_Effect : ItemEffect
{
    [SerializeField] private int bloodChance;
    public override void ExecuteEffect(EnemyStats _enemyStats, PlayerStats _playerStats)
    {
        if (Random.Range(0, 100) < bloodChance)
            _enemyStats.ApplyBlood();
    }
}
