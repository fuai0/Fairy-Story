using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            PanelManager.instance.ShowPanel("按F键进入小红帽世界!");
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
            StartCoroutine(ChangeScene());

        if (isActivate)
            ps.Play();
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(.4f);
        PanelManager.instance.HidePanel();
        SceneManager.LoadScene(worldName.text);
    }

    public void ActivateDoor()
    {
        isActivate = true;
        buttonString.text = "选定世界";
        buttonString.color = Color.green;
    }
}
