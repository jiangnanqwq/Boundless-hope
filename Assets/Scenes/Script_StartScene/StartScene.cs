using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    //��ʼ��Ϸ
    public void OnStartGameClick()
    {
        SceneManager.LoadScene("LoadScene");
    }
}
