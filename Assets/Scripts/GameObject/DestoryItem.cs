using UnityEngine;

public class DestoryItem : MonoBehaviour
{
    [SerializeField] private int health;
    private ItemDrop itemDrop;

    private void Start()
    {
        itemDrop = GetComponent<ItemDrop>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0 )
        {
            itemDrop.GenerateDrop();
            Destroy(gameObject);
        }
    }

}
