using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public string gameSceneName = "MainMenu";

    public void ChangeScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
