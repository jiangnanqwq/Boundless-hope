using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    //¿ªÊ¼ÓÎÏ·
    public void OnStartGameClick()
    {
        SceneManager.LoadScene("Test");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
