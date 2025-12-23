using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int gemCount = 211; // 合計の数
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateText();
    }

    // 宝石から「取られたよ！」と教えてもらう関数
    public void GemCollected()
    {
        gemCount--;
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text = gemCount.ToString();
    }
}