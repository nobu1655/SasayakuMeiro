using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPepper : MonoBehaviour
{
    public int scoreValue = 1;

    void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("Player"))
        {
            ScoreManager.Instance.EatOneDot();
            Destroy(gameObject);
        }
    }
}
