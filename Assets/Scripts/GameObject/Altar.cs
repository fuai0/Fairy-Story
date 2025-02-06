using UnityEngine;

public class Altar : InteractiveItem
{
    private void Start()
    {
        panelText = "°´F¼ü´ò¿ª¼ÀÌ³!";
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.F))
            UI.instance.SwitchCraftUI();
    }
}
