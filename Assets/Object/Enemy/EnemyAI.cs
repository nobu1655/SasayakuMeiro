using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;

    public float chaseDistance = 40f;
    public float stopChaseDistance = 48f;

    public float attackRange = 2.0f;
    public float attackCooldown = 1.5f;

    private float attackTimer = 0f;

    private EnemyAttack enemyAttack;

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentIndex = 0;
    private bool isChasing = false;
    private bool isAttacking = false;
    private Animator anim;

    // ★追加：追跡前に向かっていた巡回ポイントを保存する
    private int savedIndex = 0;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToNextPoint();
        anim = GetComponent<Animator>();

        enemyAttack = GetComponentInChildren<EnemyAttack>();
        enemyAttack.enabled = false;
    }

    void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                isAttacking = false;
                agent.isStopped = false;

                enemyAttack.enabled = false;

                // 巡回に戻す
                isChasing = false;
                agent.SetDestination(patrolPoints[currentIndex].position);
            }
            return;
        }

        attackTimer -= Time.deltaTime;

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
            if (dist <= attackRange)
            {
                agent.isStopped = true;
                anim.SetFloat("Speed", 0);

                LookAtPlayer();

                if (attackTimer <= 0f)
                {
                    isAttacking = true;
                    agent.isStopped = true;

                    anim.SetTrigger("Attack");
                    enemyAttack.enabled = true;

                    attackTimer = attackCooldown;
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }
        else
        {
            Patrol();
        }

        anim.SetFloat("Speed", agent.velocity.magnitude);
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

    void LookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f; // 高さ方向は無視

        if (dir == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * 10f // 向く速さ
        );
    }
}
