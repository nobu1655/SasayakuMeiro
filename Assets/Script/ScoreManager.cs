using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
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
        Debug.Log("écÇËÇÃå¬êî" + remainingDots + "/" + totalDots);
    }
}
