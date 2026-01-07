using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenechange : MonoBehaviour
{
    public string NextSceneName;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
