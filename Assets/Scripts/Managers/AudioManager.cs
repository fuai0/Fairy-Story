using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBgm;
    private int bgmIndex;

    private void Awake()
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
        if (!playBgm)
            StopAllBgm();
        else
        {
            if (!bgm[bgmIndex].isPlaying)
                PlayBgm(bgmIndex);
        }

        bgmIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void PlaySfx(int _sfxIndex, Transform _source = null)
    {
        if (sfx[_sfxIndex].isPlaying)
            return;

        if (_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].pitch = Random.Range(.85f, 1.15f);
            sfx[_sfxIndex].Play();
        }
    }
    public void StopSfx(int _index) => sfx[_index].Stop();

    public void PlayBgm(int _bgmIndex)
    {
        if (_bgmIndex >= bgm.Length)
            return;

        StopAllBgm();

        bgm[_bgmIndex].Play();
    }

    public void StopAllBgm()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
