using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;

    public float chaseDistance = 40f;
    public float stopChaseDistance = 48f;

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentIndex = 0;
    private bool isChasing = false;

    // ★追加：追跡前に向かっていた巡回ポイントを保存する
    private int savedIndex = 0;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToNextPoint();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        // --- 追跡開始 ---
        if (!isChasing && dist < chaseDistance)
        {
            isChasing = true;

            // ★追跡に入った瞬間の巡回indexを保存！
            savedIndex = (currentIndex == 0) ? patrolPoints.Length - 1 : currentIndex - 1;
            // ↑こうしないと「既に++された後のindex」になるため
        }

        // --- 追跡終了 ---
        if (isChasing && dist > stopChaseDistance)
        {
            isChasing = false;

            // savedIndex のポイントへ戻す
            agent.SetDestination(patrolPoints[savedIndex].position);

            // ★重要：巡回再スタート位置を savedIndex の次に設定
            currentIndex = (savedIndex + 1) % patrolPoints.Length;

            return;
        }

        // --- 追跡中 ---
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

        // 次のポイントへ進める（元のまま）
        currentIndex = (currentIndex + 1) % patrolPoints.Length;
    }
}
