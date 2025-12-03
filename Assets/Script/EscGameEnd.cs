using UnityEngine;

public class ExitOnEscape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // エディタの場合は停止
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // ビルドしたゲームを終了
            Application.Quit();
#endif
        }
    }
}
