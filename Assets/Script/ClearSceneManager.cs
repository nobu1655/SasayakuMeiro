using UnityEngine;
using TMPro;

public class ClearSceneManager : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI timeText;

    void Start()
    {
        rankText.text = "Rank: " + ResultData.rank;

        float timeLeft = ScoreManager.Instance != null ? (float)Timer.Instance.GetRemainingSeconds() : 0;
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timeText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }
}
