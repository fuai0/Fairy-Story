using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Sprite growth;
    private SpriteRenderer image;
    private Animator anim;

    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            anim.SetBool("Activate", true);      
    }

    public void GrowUpTrigger()
    {
        image.sprite = growth;
    }

}
