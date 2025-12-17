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

    [Header("Vision")]
    public float viewDistance = 15f; //視界距離
    public float viewAngle = 60f;　　//視野角
    public LayerMask obstacleMask;

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
        if (!isChasing && CanSeePlayer())
        {
            isChasing = true;
            savedIndex = (currentIndex == 0) ? patrolPoints.Length - 1 : currentIndex - 1;
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

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 距離チェック
        if (distanceToPlayer > viewDistance)
            return false;

        // 視野角チェック
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > viewAngle)
            return false;

        // Ray で遮蔽物チェック
        Ray ray = new Ray(transform.position + Vector3.up * 1.5f, dirToPlayer);
        if (Physics.Raycast(ray, out RaycastHit hit, viewDistance, ~obstacleMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
    
    //視界のの可視化
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 left = Quaternion.Euler(0, -viewAngle, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + left * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + right * viewDistance);
    }
}
