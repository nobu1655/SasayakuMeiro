using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatPepper : MonoBehaviour
{
    public int scoreToDecrease = 1;
    public GameObject mapObject;
    private ScoreManager scoreManager;

    void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("Player"))
        {
            if (scoreManager != null)
            {
                scoreManager.DecreaseScore(scoreToDecrease);
            }
            Destroy(mapObject);
            Destroy(gameObject);
        }
    }
}
