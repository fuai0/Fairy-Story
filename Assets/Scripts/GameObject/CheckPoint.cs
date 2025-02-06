using UnityEngine;

public class CheckPoint : InteractiveItem
{
    [SerializeField] private Sprite growth;
    private SpriteRenderer image;
    private Animator anim;
    public string id;
    public bool activated;

    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        panelText = "按F键使用休息点!";
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.F))
            UI.instance.SwitchCheckMenuUI();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            ActivateCheckpoint();

        base.OnTriggerEnter2D(collision);
    }

    public void GrowUpTrigger() => image.sprite = growth;

    public void ActivateCheckpoint()
    {
        activated = true;
        anim.SetBool("Activate", true);
    }
}
