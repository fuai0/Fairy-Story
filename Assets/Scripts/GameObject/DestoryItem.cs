using System.Collections;
using UnityEngine;

public class DestoryItem : MonoBehaviour
{
    [SerializeField] private int health;
    private ItemDrop itemDrop;
    private float positionXBorn;

    private void Start()
    {
        itemDrop = GetComponent<ItemDrop>();
        positionXBorn = transform.position.x;
    }

    public void TakeDamage(int damage)
    {
        AudioManager.instance.PlaySfx(2);

        transform.position = new Vector2(transform.position.x + .1f, transform.position.y + .1f);
        InvokeRepeating("HittedFX", .05f, .05f);
        StartCoroutine(StopHittedFX());

        health -= damage;
        if(health <= 0 )
        {
            itemDrop.GenerateDrop();
            Destroy(gameObject);
        }
    }

    private void HittedFX()
    {
        if (transform.position.x > positionXBorn)
            transform.position = new Vector2(transform.position.x - .2f, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x + .2f, transform.position.y);
    }

    private IEnumerator StopHittedFX()
    {
        yield return new WaitForSeconds(.3f);
        CancelInvoke("HittedFX");

        transform.position = new Vector2(transform.position.x - .1f, transform.position.y - .1f);
    }
}
