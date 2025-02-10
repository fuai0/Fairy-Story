using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveManager
{
    public static GameManager instance;
   
    private List<CheckPoint> checkPoints;

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

        checkPoints = FindAllCheckPoints();
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

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SaveData saveData = new SaveData();
        CheckPoint closestCheckPoint = FindClosestCheckpoint(); 

        SaveCheckPoints(ref saveData);
        Debug.Log(saveData.checkPoints.Count);
        ChangeSence(scene.name);

        StartCoroutine(DelayRestart(saveData, closestCheckPoint));

    }

    private IEnumerator DelayRestart(SaveData saveData,CheckPoint closestCheckPoint)
    {
        yield return new WaitForSeconds(.6f);
        Debug.Log(saveData.checkPoints.Count);
        LoadCheckPoints(saveData);

        if(closestCheckPoint != null)
            PlayerManager.instance.player.transform.position = closestCheckPoint.transform.position; 
    }

    private void SaveCheckPoints(ref SaveData saveData)
    {
        foreach (var checkpoint in checkPoints)
            saveData.checkPoints.Add(checkpoint.id, checkpoint.activated);
    }

    private void LoadCheckPoints(SaveData saveData)
    {
        foreach (var pair in saveData.checkPoints)
        {
            foreach (var checkpoint in checkPoints)
            {
                if (pair.Key == checkpoint.id && pair.Value == true)
                    checkpoint.ActivateCheckpoint();
            }
        }
    }

    private List<CheckPoint> FindAllCheckPoints()
    {
        IEnumerable<CheckPoint> CheckPoints = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<CheckPoint>();
        return new List<CheckPoint>(CheckPoints);
    }
}
