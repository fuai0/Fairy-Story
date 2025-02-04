using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option_UI : MonoBehaviour
{
    private TMP_Dropdown dropDown;

    private void Start()
    {
        dropDown = GetComponentInChildren<TMP_Dropdown>();
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetResolution()
    {
        switch (dropDown.value)
        {
            case 0: Screen.SetResolution(1920, 1080, false); break;
            case 1: Screen.SetResolution(1280, 720, false); break;
            case 2: Screen.SetResolution(720, 480, false); break;
        }
    }
}
