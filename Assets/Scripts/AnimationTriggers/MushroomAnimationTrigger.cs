using UnityEngine;

public class MushroomAnimationTrigger : MonoBehaviour
{
    private Mushroom enemy => GetComponentInParent<Mushroom>();

    private void AttackFinish() => enemy.AttackFinish();

    private void HittedFinish() => enemy.HittedFinish();

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackPosition, enemy.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                Player player = hit.GetComponent<Player>();
                enemy.stats.DoDamage(player.stats);
            }
        }
    }

    private void DeadTrigger()
    {
        Destroy(enemy.gameObject);
    }
}
