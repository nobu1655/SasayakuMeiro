using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepVolume : MonoBehaviour
{
    public Transform Cylinder;        // プレイヤーを入れる
    public AudioSource footstep;    // 足音のAudioSource
    public float maxDistance = 20f; // この距離以上は聞こえない

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Cylinder.position);

        // 0〜1に正規化（distance 0 → 1、maxDistance → 0）
        float volume = Mathf.Clamp01(1f - (distance / maxDistance));

        footstep.volume = volume;
    }
}
