using UnityEngine;
using DG.Tweening;

public class MenuAnimator : MonoBehaviour
{
    [SerializeField]
    private RectTransform backBtn,
        recordBtn,
        settingsBtn,
        playWindow,
        settingsWindow,
        recordWindow;

    private Sequence showSettings,
        showRecord,
        backMain;

    private void Start()
    {
        InitAnims();
    }

    private void InitAnims()
    {
        showSettings = DOTween
            .Sequence()
            .Append(backBtn.DOAnchorPosY(-25, 0.5f))
            .Join(recordBtn.DOAnchorPosY(60, 0.5f))
            .Join(settingsBtn.DOAnchorPosY(60, 0.5f))
            .Join(playWindow.DOAnchorPosY(-400, 0.5f))
            .Join(settingsWindow.DOAnchorPosX(0, 0.5f));

        showRecord = DOTween
            .Sequence()
            .Append(backBtn.DOAnchorPosY(-25, 0.5f))
            .Join(recordBtn.DOAnchorPosY(60, 0.5f))
            .Join(settingsBtn.DOAnchorPosY(60, 0.5f))
            .Join(playWindow.DOAnchorPosY(-400, 0.5f))
            .Join(recordWindow.DOAnchorPosX(0, 0.5f));

        backMain = DOTween
            .Sequence()
            .Append(backBtn.DOAnchorPosY(60, 0.5f))
            .Join(recordBtn.DOAnchorPosY(-10, 0.5f))
            .Join(settingsBtn.DOAnchorPosY(-10, 0.5f))
            .Join(playWindow.DOAnchorPosY(0, 0.5f))
            .Join(settingsWindow.DOAnchorPosX(300, 0.5f))
            .Join(recordWindow.DOAnchorPosX(-300, 0.5f));
    }

    public void ShowSettings()
    {
        showSettings.Restart();
    }

    public void ShowRecord()
    {
        showRecord.Restart();
    }

    public void BackMain()
    {
        backMain.Restart();
    }
}
