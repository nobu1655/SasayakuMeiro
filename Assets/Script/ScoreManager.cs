using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int gemCount = 210; // 合計の数
    public TextMeshProUGUI scoreText;

    bool isCleared = false;
    void Start()
    {
        UpdateText();
    }

    // 宝石から「取られたよ！」と教えてもらう関数
    public void GemCollected()
    {
        if (isCleared) return;
        gemCount--;
        UpdateText();

        if(gemCount==0)
        {
            isCleared = true;
            SceneManager.LoadScene("CleareScene");
        }
    }

    void UpdateText()
    {
        scoreText.text = gemCount.ToString();
    }
}