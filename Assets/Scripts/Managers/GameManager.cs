using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
        
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeSence(string worldName)
    {
        StartCoroutine(DelayChange(worldName));
    }

    private IEnumerator DelayChange(string worldName)
    {
        yield return new WaitForSeconds(.4f);
        SaveData saveData = new SaveData();

        Inventory.instance.SaveData(ref saveData);

        SceneManager.LoadScene(worldName);

        StartCoroutine(DelayLoad(saveData));
    }

    private IEnumerator DelayLoad(SaveData saveData)
    {
        yield return new WaitForSeconds(.1f);

        Inventory.instance.LoadData(saveData);
    }

}
