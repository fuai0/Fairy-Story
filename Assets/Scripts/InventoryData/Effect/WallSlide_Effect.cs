using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effext", menuName = "Data/Item Effect/WallSlide Effext")]

public class WallSlide_Effect : ItemEffect
{
    public override void EquipEffect()
    {
        PlayerManager.instance.player.canWallSlide = true;
    }

    public override void UnEquipEffect()
    {
        PlayerManager.instance.player.canWallSlide = false;
    }
}
