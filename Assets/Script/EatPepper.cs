using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPepper : MonoBehaviour
{
  void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
