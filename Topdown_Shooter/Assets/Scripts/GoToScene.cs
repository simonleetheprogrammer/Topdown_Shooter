using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    public void GoToSceneByName()
    {
        SceneManager.LoadScene(sceneName);
    }
}
