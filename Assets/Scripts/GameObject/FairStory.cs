using UnityEngine;

public class FairStory : InteractiveItem
{
    private void Start()
    {
        panelText = "按F键打开童话书!";
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.F))
            UI.instance.SwitchWorldUI();
    }
}
