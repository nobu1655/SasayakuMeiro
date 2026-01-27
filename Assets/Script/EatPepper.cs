using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class EatPepper : MonoBehaviour
{
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
            ScoreManager.Instance.GemCollected();
            Destroy(mapObject);
            Destroy(gameObject);
        }
    }
}
