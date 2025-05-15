using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    const string GAME_SCENE_STRING = "GameScene";

    public void StartGame()
    {
        SceneManager.LoadScene(GAME_SCENE_STRING);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
