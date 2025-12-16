using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public Transform playerStartPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // CharacterController ‚ª‚ ‚éê‡‚Íˆê“x–³Œø‰»
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }

            // ‰ŠúˆÊ’u‚É–ß‚·
            other.transform.position = playerStartPoint.position;

            // Ä‚Ñ—LŒø‰»
            if (cc != null)
            {
                cc.enabled = true;
            }
        }
    }
}
