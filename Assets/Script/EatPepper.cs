using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPepper : MonoBehaviour
{
    public int scoreValue = 1;
    public GameObject mapObject;

    void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("Player"))
        {
            //if (GemEventManager.Instance != null)
            //{
            //    GemEventManager.Instance.CollectGem(uniqueGemID);
            //}

            ScoreManager.Instance.EatOneDot();
            Destroy(mapObject);
            Destroy(gameObject);
        }
    }
}
