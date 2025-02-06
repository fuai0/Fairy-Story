using TMPro;
using UnityEngine;

public class Door : InteractiveItem
{
    [SerializeField] private TextMeshProUGUI worldName;
    [SerializeField] private TextMeshProUGUI buttonString;
    private bool isActivate;
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();

        isActivate = false;
        panelText = "��F������С��ñ����!";
        ps.Stop();
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.F))
        {
            PanelManager.instance.HidePanel();
            GameManager.instance.ChangeSence(worldName.text);
        }

        if (isActivate)
            ps.Play();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && isActivate)
        {
            PanelManager.instance.ShowPanel(panelText);
            isInsideTrigger = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && isActivate)
        {
            PanelManager.instance.HidePanel();
            isInsideTrigger = false;
        }
    }

    public void ActivateDoor()
    {
        isActivate = true;
        buttonString.text = "ѡ������";
        buttonString.color = Color.green;
    }
}
