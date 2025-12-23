using UnityEngine;
using TMPro; // TextMeshProを使用するために必要

public class ScoreManager : MonoBehaviour
{
    [Header("UI設定")]
    [Tooltip("画面上のスコア表示に使うTextMeshProUGUIコンポーネントをここにドラッグ＆ドロップしてください。")]
    public TextMeshProUGUI scoreText;

    [Header("初期値設定")]
    [Tooltip("ゲーム開始時の初期スコア（宝石の総数など）")]
    [SerializeField] // privateな変数でもインスペクタに表示・設定できるようにする
    private int initialScore = 10;

    // 現在のスコアを保持する変数
    private int currentScore;

    // スコア管理用オブジェクトが一つだけ存在するかを確認
    private static ScoreManager instance;

    void Awake()
    {
        // シーンにScoreManagerが複数存在しないようにチェックする処理
        if (instance == null)
        {
            instance = this;
            // シーンをまたいで持ち越したい場合は DontDestroyOnLoad(gameObject); を使用しますが、今回はスキップします。
        }
        else
        {
            // 既に存在する場合は自分自身を破棄
            Destroy(gameObject);
            return;
        }

        // currentScoreを初期スコアに設定
        currentScore = initialScore;
    }

    void Start()
    {
        // 初回起動時にUIの参照があるかチェックし、スコアを初期表示
        if (scoreText == null)
        {
            Debug.LogError("ScoreManager: scoreText (TextMeshProUGUI)が設定されていません。インスペクタで設定を確認してください。");
        }

        UpdateScoreText();
        Debug.Log("ScoreManager 初期化完了。初期スコア: " + currentScore);
    }

    /// <summary>
    /// スコアを減らし、UIを更新する
    /// </summary>
    /// <param name="amount">減らすスコアの量 (例: 1)</param>
    public void DecreaseScore(int amount)
    {
        // ログ: 呼び出しが成功したことを確認
        Debug.Log($"ScoreManager: DecreaseScore 呼び出し成功。減らす量: {amount}, 変更前スコア: {currentScore}");

        // スコアを減算
        currentScore -= amount;

        // スコアがマイナスにならないようにガード
        if (currentScore < 0)
        {
            currentScore = 0;
        }

        // UIを更新
        UpdateScoreText();

        // ログ: スコアが更新されたことを確認
        Debug.Log($"ScoreManager: スコア更新完了。現在のスコア: {currentScore}");

        // 必要に応じて、スコアが0になった時の処理（ゲームクリアなど）をここに追加できます
        // if (currentScore == 0) { ... }
    }

    /// <summary>
    /// TextMeshProの表示を現在のスコア値で更新する
    /// </summary>
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            // TextMeshProのテキストを現在のスコア値で更新
            scoreText.text = currentScore.ToString();
        }
        // UIが設定されていない場合のチェックはStart()で行っているため、ここでは省略可能
    }
}