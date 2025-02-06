using UnityEngine;

public class FairStory : InteractiveItem
{
    private void Start()
    {
        panelText = "��F����ͯ����!";
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.F))
            UI.instance.SwitchWorldUI();
    }
}
