using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private WindowManager wm;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private RectTransform endWindow;

    [SerializeField]
    private TextMeshProUGUI endText;

    [SerializeField]
    private TextMeshProUGUI scoreCounter;
    private int scores;

    private List<List<int>> lines = new List<List<int>>()
    {
        new List<int>(),
        new List<int>(),
        new List<int>()
    };

    private void Start()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            lines[0].Add(blocks[i].blockId);
        }
    }

    public bool CheckCorrectPlace(int blockId, int place)
    {
        if (lines[place].Count > 0)
        {
            return lines[place].LastElement<int>() > blockId;
        }
        return true;
    }

    public bool CheckCanDrag(int blockId, int place)
    {
        if (lines[place].Count > 0)
        {
            return lines[place].LastElement<int>() == blockId;
        }
        return false;
    }

    public void PlaceBlock(int blockId, int place, int oldPlace)
    {
        if (place != oldPlace)
        {
            scores++;
            scoreCounter.text = scores.ToString();
        }
        lines[oldPlace].Remove(blockId);
        lines[place].Add(blockId);
        CheckWin();
    }

    private void CheckWin()
    {
        for (int i = 1; i < lines.Count; i++)
        {
            if (lines[i].Count == 4)
            {
                int record = PlayerPrefs.GetInt("Record");
                if (record == 0 || scores < record)
                {
                    PlayerPrefs.SetInt("Record", scores);
                    PlayerPrefs.Save();
                }
                else
                {
                    endText.text = "No new record";
                }
                endWindow.DOAnchorPosY(0, 0.5f).Play();
                wm.EndGame();
                return;
            }
        }
    }
}
