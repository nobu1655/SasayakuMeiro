using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySE : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip[] footstepClips;
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;
    public float minInterval = 0.05f; // 同フレーム連打防止
    private float lastPlayTime = -1f;

    public void FootStep()
    {
        if (footstepSource == null || footstepClips == null || footstepClips.Length == 0) return;

        // 連打防止（稀に同フレームで複数のイベントが来る場合）
        if (Time.time - lastPlayTime < minInterval) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepSource.pitch = Random.Range(minPitch, maxPitch);
        footstepSource.PlayOneShot(clip);
        lastPlayTime = Time.time;
    }
}
