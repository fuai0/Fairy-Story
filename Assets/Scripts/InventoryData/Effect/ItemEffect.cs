using UnityEngine;

public class ItemEffect : ScriptableObject
{
    public string effectName;
    [TextArea]
    public string effectDescription;
    public virtual void ExecuteEffect(EnemyStats _enemyStats, PlayerStats _playerStats)
    {
    }

    public virtual void EquipEffect()
    {

    }

    public virtual void UnEquipEffect()
    {

    }
}
