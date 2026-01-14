using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton1 : MonoBehaviour
{
    public string NextSceneName;

    // Update is called once per frame
    public void OnButtonClick()
    {
        SceneManager.LoadScene(NextSceneName);
        Debug.Log("ok");
    }
}
