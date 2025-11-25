using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;      // ����|�C���g
    public Transform player;              // �v���C���[

    public float chaseDistance = 8f;      // �ǐՊJ�n����
    public float stopChaseDistance = 12f; // �ǐՉ�������

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToNextPoint();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        // �ǐՊJ�n
        if (!isChasing && dist < chaseDistance)
        {
            isChasing = true;
        }

        // �ǐՉ���
        if (isChasing && dist > stopChaseDistance)
        {
            isChasing = false;
            GoToNextPoint();
        }

        // ��Ԃɂ���čs���ؑ�
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

        // ���̃|�C���g�ցi�Ō�܂ōs�����烋�[�v�j
        currentIndex = (currentIndex + 1) % patrolPoints.Length;
    }
}
