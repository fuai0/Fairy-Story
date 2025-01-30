using System.Collections;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject optionUI;

    [SerializeField] private GameObject majorStatsUI;
    [SerializeField] private GameObject statsUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Tab))
            SwitchWithKeyTo(characterUI);

        if (Input.GetKeyDown(KeyCode.I))
            SwitchWithKeyTo(inventoryUI);

        if (Input.GetKeyDown(KeyCode.M))
            SwitchWithKeyTo(mapUI);

        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchWithKeyTo(optionUI);
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

         _menu.SetActive(true);
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            return;
        }

        SwitchTo(_menu);
    }

    public void SwitchStatsUI(GameObject _statsUI)
    {
        majorStatsUI.SetActive(false);
        statsUI.SetActive(false);

        _statsUI.SetActive(true);
    }
}

