using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WindowManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform window;

    private TextMeshProUGUI windowText;
    private RectTransform rt;

    private string hintText;
    private string recordText = "Current record:\n";
    private string currentString;
    private Sequence showWindow,
        hideWindow;
    private int record;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        windowText = window.GetComponentInChildren<TextMeshProUGUI>();
        hintText = windowText.text;
        InitAnim();
        record = PlayerPrefs.GetInt("Record");
        if (record == 0)
        {
            ShowHelp();
        }
    }

    private void InitAnim()
    {
        showWindow = DOTween.Sequence();
        showWindow
            .AppendCallback(() =>
            {
                windowText.text = currentString;
            })
            .Append(window.DOAnchorPosY(0, 0.5f));

        hideWindow = DOTween.Sequence().Append(window.DOAnchorPosY(100, 0.5f));
    }

    public void ShowHelp()
    {
        currentString = hintText;
        showWindow.Restart();
    }

    public void ShowRecord()
    {
        if (record > 0)
        {
            currentString = recordText + record;
        }
        else
        {
            currentString = recordText + "has not been set yet";
        }
        showWindow.Restart();
    }

    private void Update()
    {
        if (
            Input.GetMouseButtonDown(0)
            && window.anchoredPosition.y < 100
            && !hideWindow.IsPlaying()
            && !showWindow.IsPlaying()
        )
        {
            hideWindow.Restart();
        }
    }

    public void EndGame()
    {
        transform.DOMoveY(transform.position.y + 10, 0.5f).Play();
    }
}
