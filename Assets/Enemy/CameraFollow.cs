using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerBody;       // プレイヤーのTransform
    public float mouseSensitivity = 10f; // ← 回転スピード（大きいほど速い）

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウス固定
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 上下視点（カメラだけ動かす）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // 上下の制限

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 左右視点（プレイヤーごと回転）
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
