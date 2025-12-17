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
            PlayerDamage damage = other.GetComponent<PlayerDamage>();
            if (damage != null)
            {
                damage.OnHit();
            }
        }
    }
}