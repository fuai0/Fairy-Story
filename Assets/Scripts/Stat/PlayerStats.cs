using UnityEngine;

public class PlayerStats : CharacterStats
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Die()
    {
        base.Die();

        UI.instance.ShowDeadUI();
    }
}
