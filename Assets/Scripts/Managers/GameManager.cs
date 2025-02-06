using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveManager
{
    public static GameManager instance;
   
    [SerializeField] private CheckPoint[] checkPoints;

    private Player player;

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

    private void Update()
    {
        if (PlayerManager.instance != null)
            player = PlayerManager.instance.player;
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

    public void LoadData(SaveData _data)
    {
        player.transform.position = _data.playerPosition;

        foreach (var pair in _data.checkPoints)
        {
            foreach (var checkpoint in checkPoints)
            {
                if (pair.Key == checkpoint.id && pair.Value == true)
                    checkpoint.ActivateCheckpoint();
            }
        }

        foreach (var checkpoint in checkPoints)
        {
            if (_data.closestCheckPointId == checkpoint.id)
                player.transform.position = checkpoint.transform.position;
        }
    }

    public void SaveData(ref SaveData _data)
    {
        _data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        _data.playerPosition = PlayerManager.instance.player.transform.position;

        if (FindClosestCheckpoint() != null)
            _data.closestCheckPointId = FindClosestCheckpoint().id;

        _data.checkPoints.Clear();

        foreach (var checkpoint in checkPoints)
            _data.checkPoints.Add(checkpoint.id, checkpoint.activated);
    }

    private CheckPoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        CheckPoint closestCheckPoint = null;

        foreach (var checkpoint in checkPoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.transform.position, checkpoint.transform.position);

            if (distanceToCheckpoint < closestDistance && checkpoint.activated)
            {
                closestCheckPoint = checkpoint;
                closestDistance = distanceToCheckpoint;
            }
        }

        return closestCheckPoint;
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
