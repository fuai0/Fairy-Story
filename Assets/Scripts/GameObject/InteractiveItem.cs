using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    protected bool isInsideTrigger = false;
    protected string panelText;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PanelManager.instance.ShowPanel(panelText);
            isInsideTrigger = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PanelManager.instance.HidePanel();
            isInsideTrigger = false;
        }
    }
}
