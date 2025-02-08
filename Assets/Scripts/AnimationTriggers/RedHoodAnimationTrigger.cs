using UnityEngine;

public class RedHoodAnimationTriggers : MonoBehaviour
{
    private RedHood player => GetComponentInParent<RedHood>();

    private void AttackFinish() => player.AttackFinish();

    private void HittedFinish() => player.HittedFinish();

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackPosition, player.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                Enemy enemy = hit.GetComponent<Enemy>();

                enemy.hittedDir = player.facingDir;
                player.stats.DoDamage(enemy.stats);

                ItemData_Equipment weaponData = Inventory.instance.GetEquip(EquipmentType.Weapon);

                if (weaponData != null)
                    weaponData.Effect(enemy.stats as EnemyStats, null);
            }

            if(hit.GetComponent<DestoryItem>() != null)
            {
                DestoryItem destoryItem = hit.GetComponent<DestoryItem>();

                destoryItem.TakeDamage(player.stats.damage.GetValue());
            }
        }
    }

    private void CreateArrow()
    {
        player.CreateArrow();
    }
}