using UnityEngine;

public class Altar : InteractiveItem
{
    private void Start()
    {
        panelText = "��F���򿪼�̳!";
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.F))
            UI.instance.SwitchCraftUI();
    }
}
