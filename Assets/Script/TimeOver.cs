using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public string gameOverSceneName = "OverScene";

    public void GoToGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}

