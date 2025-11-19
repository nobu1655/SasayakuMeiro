using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;      
    public Transform player;              

    public float chaseDistance = 160f;     
    public float stopChaseDistance = 192f; 

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentIndex = 0;
    private bool isChasing = false;

    private int savedIndex = 0;
    
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToNextPoint();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        
        if (!isChasing && dist < chaseDistance)
        {
            isChasing = true;

            savedIndex = (currentIndex == 0) ? patrolPoints.Length - 1 : currentIndex - 1;
        }

        
        if (isChasing && dist > stopChaseDistance)
        {
            isChasing = false;

            // savedIndex のポイントへ戻す
            agent.SetDestination(patrolPoints[savedIndex].position);

            // ★重要：巡回再スタート位置を savedIndex の次に設定
            currentIndex = (savedIndex + 1) % patrolPoints.Length;

            return;
        }

       
        if (isChasing)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentIndex].position);

       
        currentIndex = (currentIndex + 1) % patrolPoints.Length;
    }
}
