using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;          // InspectorでPlayerをアサイン
    public float moveSpeed = 2f;      // 移動速度
    public float attackRange = 1.5f;  // 攻撃開始距離
    public float attackCooldown = 1.5f; // 攻撃間隔（秒）

    Animator animator;
    float nextAttackTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // 水平方向の距離（Y軸は無視して平面で判断）
        Vector3 flatSelf = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatPlayer = new Vector3(player.position.x, 0f, player.position.z);
        float distance = Vector3.Distance(flatSelf, flatPlayer);

        if (distance > attackRange)
        {
            // プレイヤーへ移動
            Vector3 direction = (flatPlayer - flatSelf).normalized;
            // 身体はプレイヤー方向を向くがY回転のみ
            Vector3 lookTarget = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookTarget);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            // Animator に歩いていることを通知
            // ここは 0..1 スケールで渡している（1 = 歩き）
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            // 攻撃範囲内
            animator.SetFloat("Speed", 0f);

            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("Attack");
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}
