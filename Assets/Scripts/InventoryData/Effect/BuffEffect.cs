using UnityEngine;

[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff Effect")]
public class Buff_Effect : ItemEffect
{
    private PlayerStats stats;
    [SerializeField] private StatType buffType;
    [SerializeField] private int buffAmount;
    [SerializeField] private float buffDuration;


    public override void ExecuteEffect(EnemyStats _enemyStats, PlayerStats _playerStats)
    {
        _playerStats.IncreaseStat(buffAmount, buffDuration, _playerStats.GetStat(buffType));
    }
}
