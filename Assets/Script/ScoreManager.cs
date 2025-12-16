using UnityEngine;
using TMPro; // TextMeshProを使用するために必要

public class ScoreManager : MonoBehaviour
{
    // インスペクターからTextMeshProのコンポーネントをD&Dで設定
    public TextMeshProUGUI scoreNumberText;

    // 現在のスコア（宝石の残り数など）
    private int currentScore = 208;

    void Start()
    {
        // 初期表示を更新
        UpdateScoreDisplay();
    }

    // 外部からスコアを減らすためのパブリックメソッド
    public void DecreaseScore(int amount)
    {
        currentScore -= amount;

        // 表示を更新
        UpdateScoreDisplay();
    }

    // TextMeshProの表示を更新するメソッド
    private void UpdateScoreDisplay()
    {
        if (scoreNumberText != null)
        {
            scoreNumberText.text = currentScore.ToString();
        }
    }
}