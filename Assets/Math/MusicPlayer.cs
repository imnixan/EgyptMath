using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource player;

    private void Awake()
    {
        player = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            player.volume = 0.2f;
            PlayAgain();
        }
    }

    public void PlayAgain()
    {
        player.Play();
    }

    public void PausePlay()
    {
        player.Pause();
    }
}
