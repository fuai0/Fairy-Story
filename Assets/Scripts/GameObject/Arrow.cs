using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;

    public float moveSpeed;
    public float liveTime;
    private float liveTimer = 0;

    private int moveDir;

    private void Start()
    {
        player = PlayerManager.instance.player;
        rb = GetComponent<Rigidbody2D>();

        moveDir = player.facingDir;
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(moveSpeed * moveDir, 0);
        liveTimer += Time.deltaTime;

        if (liveTimer > liveTime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            AudioManager.instance.PlaySfx(1);

            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.hittedDir = moveDir;
            player.stats.DoDamage(enemy.stats);
            Destroy(gameObject);
        }
    }
}
