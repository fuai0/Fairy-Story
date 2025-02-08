using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effext", menuName = "Data/Item Effect/Shoot Effext")]

public class Shoot_Effect : ItemEffect
{
    public override void EquipEffect()
    {
        PlayerManager.instance.player.canShoot = true;
    }

    public override void UnEquipEffect()
    {
        PlayerManager.instance.player.canShoot = false;
    }
}
