using UnityEngine;

public class FairStory : MonoBehaviour
{
    private bool isInsideTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            PanelManager.instance.ShowPanel("按F键打开童话书!");
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
            UI.instance.SwitchWorldUI();
    }
}
