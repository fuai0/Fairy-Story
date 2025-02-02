using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    { 
        HidePanel();
    }

    public void ShowPanel(string message)
    {
        panel.SetActive(true);
        messageText.text = message;
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}

