using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI worldName;
    [SerializeField] private TextMeshProUGUI buttonString;
    private bool isInsideTrigger = false;
    private bool isActivate;
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();

        isActivate = false;
        ps.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivate && collision.gameObject.GetComponent<Player>() != null)
        {
            PanelManager.instance.ShowPanel("��F������С��ñ����!");
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActivate && collision.GetComponent<Player>() != null)
        {
            PanelManager.instance.HidePanel();
            isInsideTrigger = false;
        }
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

    public void ActivateDoor()
    {
        isActivate = true;
        buttonString.text = "ѡ������";
        buttonString.color = Color.green;
    }
}
