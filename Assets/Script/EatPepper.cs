using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatPepper : MonoBehaviour
{
    public int scoreToDecrease = 1;
    public GameObject mapObject;
    private ScoreManager ScoreManager;

    void Start()
    {
        // シーン内の ScoreManager スクリプトを探して参照を取得する
        // 取得した参照を private な ScoreManager 変数に格納する
        ScoreManager = FindObjectOfType<ScoreManager>();

        if (ScoreManager == null)
        {
            Debug.LogError("EatPepper: ScoreManagerが見つかりません。シーンにScoreManagerがアタッチされているか確認してください。");
        }
        else
        {
            Debug.Log("EatPepper: ScoreManagerの参照を正常に取得しました。");
        }
    }
    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
            if (ScoreManager != null)
            {
                ScoreManager.DecreaseScore(1);
            }
            Destroy(mapObject);
            Destroy(gameObject);
        }
    }
}
