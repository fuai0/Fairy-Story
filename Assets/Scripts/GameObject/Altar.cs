using UnityEngine;

public class Altar : MonoBehaviour
{
    bool isInsideTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            PanelManager.instance.ShowPanel("°´F¼ü´ò¿ª¼ÀÌ³!");
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PanelManager.instance.HidePanel();
            isInsideTrigger = false;
        }
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.F))
            UI.instance.SwitchCraftUI();
    }
}
