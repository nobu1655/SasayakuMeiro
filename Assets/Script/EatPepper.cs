using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class EatPepper : MonoBehaviour
{
    public int gemCount = 212;
    public TextMeshProUGUI scoreText;
    public GameObject mapObject;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager= GameObject.FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("Player"))
        {
            scoreManager.GemCollected();
            Destroy(mapObject);
            Destroy(gameObject);
        }
    }
}
