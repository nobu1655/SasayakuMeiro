using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFootstep : MonoBehaviour
{
    public AudioSource footstepSource;
    public float footstepInterval = 0.5f; // 足音間隔
    public Transform Cylinder;              // プレイヤーをセット
    public float maxHearDistance = 30f;   // この距離以上ならほぼ聞こえない
    public float minVolume = 0.05f;       // 最小音量
    public float maxVolume = 0.5f;        // 最大音量

    private NavMeshAgent agent;
    private float timer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 足音が鳴る速度かどうか
        if (agent.velocity.magnitude > 0.2f)
        {
            timer += Time.deltaTime;

            if (timer >= footstepInterval)
            {
                PlayFootstepWithDistance();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    void PlayFootstepWithDistance()
    {
        float distance = Vector3.Distance(transform.position, Cylinder.position);

        // 0.0〜1.0 に正規化
        float volumeRate = 1f - Mathf.Clamp01(distance / maxHearDistance);

        // 最小〜最大音量の間に収める
        float volume = Mathf.Lerp(minVolume, maxVolume, volumeRate);

        footstepSource.volume = volume;
        footstepSource.Play();
    }
}
