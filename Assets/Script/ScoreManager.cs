using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TMP_Text ScoreText;
    public int totalDots = 105;
    public int remainingDots;

    void Awake()
    {
        Instance = this;
        remainingDots = totalDots;
    }

  public void EatOneDot()
    {
        remainingDots--;
        // Debug.Log("écÇËÇÃå¬êî" + remainingDots + "/" + totalDots);
        ScoreText.text = remainingDots + "/" + totalDots;
    }
}
