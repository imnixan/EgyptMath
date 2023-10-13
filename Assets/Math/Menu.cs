using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI recordShow;

    [SerializeField]
    private Image soundSwitcher,
        vibroSwitcher;

    [SerializeField]
    private RectTransform settngs,
        record,
        play;

    [SerializeField]
    private Sprite[] switchState;

    [SerializeField]
    private MusicPlayer musicPlayer;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 300;
    }

    private void Start()
    {
        int record = PlayerPrefs.GetInt("Record");
        if (record > 0)
        {
            recordShow.text = record.ToString();
        }
        SetIcons();
        ShowUI();
    }

    private void ShowUI()
    {
        settngs.DOAnchorPosX(-15, 0.8f).Play();
        record.DOAnchorPosX(15, 0.6f).Play();
        play.DOAnchorPosY(0, 0.3f).Play();
    }

    public void SwitchSound()
    {
        int soundState = PlayerPrefs.GetInt("Sound", 1);
        PlayerPrefs.SetInt("Sound", soundState == 1 ? 0 : 1);
        PlayerPrefs.Save();
        SetSoundIcon();
    }

    public void SwitchVibro()
    {
        int vibroState = PlayerPrefs.GetInt("Vibro", 1);
        PlayerPrefs.SetInt("Vibro", vibroState == 1 ? 0 : 1);
        PlayerPrefs.Save();
        SetVibroIcon();
    }

    private void SetIcons()
    {
        SetVibroIcon();
        SetSoundIcon();
    }

    private void SetVibroIcon()
    {
        vibroSwitcher.sprite = switchState[PlayerPrefs.GetInt("Vibro", 1)];
    }

    private void SetSoundIcon()
    {
        int soundState = PlayerPrefs.GetInt("Sound", 1);
        soundSwitcher.sprite = switchState[soundState];
        if (soundState == 1)
        {
            musicPlayer.PlayAgain();
        }
        else
        {
            musicPlayer.PausePlay();
        }
    }

    public void Game()
    {
        SceneManager.LoadScene("Pyramids");
    }
}
