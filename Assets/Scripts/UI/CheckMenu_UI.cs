using UnityEngine;

public class CheckMenu_UI : MonoBehaviour
{
    public void Rest()
    {
        PlayerManager.instance.player.stats.currentHealth = PlayerManager.instance.player.stats.GetHealth();
    }

    public void Transmission()
    {

    }

    public void BackToMain()
    {
        GameManager.instance.PauseGame(false);
        GameManager.instance.ChangeSence("MainScene");
    }
}
