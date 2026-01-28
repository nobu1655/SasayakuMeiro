using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening; // これを忘れずに追加
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;　

    public int gemCount = 210;
    public TextMeshProUGUI scoreText;
    public float rotateDuration = 0.2f; // 回転の速さ

    bool isCleared = false;

    void Awake() 
    {
        Instance = this;
    }

    void Start()
    {
        // 初期表示
        scoreText.text = gemCount.ToString();
    }

    public void GemCollected()
    {
        if (isCleared) return;
        gemCount--;
        scoreText.text = gemCount.ToString(); 

        // アニメーション付きでテキスト更新
        UpdateTextWithAnimation();

        if (gemCount <= 0)
        {
            isCleared = true;
            double timeLeft = Timer.Instance.timeRemaining;

            ResultData.rank = Score.CalculateRank((float)timeLeft);

            // 少しだけ待ってからシーン遷移させると、最後の回転が見えて綺麗です
            DOVirtual.DelayedCall(0.5f, () => SceneManager.LoadScene("CleareScene"));
        }
    }

    void UpdateTextWithAnimation()
    {
        // DOTweenのシーケンス作成
        Sequence seq = DOTween.Sequence();

        // 1. 今の数字を上に90度倒す
        seq.Append(scoreText.transform.DORotate(new Vector3(90, 0, 0), rotateDuration).SetEase(Ease.InQuad));

        // 2. 倒れた瞬間に文字を変えて、下側に配置する
        seq.AppendCallback(() => {
            scoreText.text = gemCount.ToString();
            scoreText.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        });

        // 3. 下から正面（0度）まで起き上がらせる
        seq.Append(scoreText.transform.DORotate(new Vector3(0, 0, 0), rotateDuration).SetEase(Ease.OutBack));
    }
}