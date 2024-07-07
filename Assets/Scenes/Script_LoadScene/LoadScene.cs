using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Text textInfo;
    public Slider slider;
    public Transform role;

    private void Start()
    {
        StartCoroutine(ToMainScene());
    }

    IEnumerator ToMainScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        operation.allowSceneActivation = false;
       
        while (!operation.isDone)
        {
            
            if (operation.progress >= 0.9f)
            {
                role.transform.position = new Vector3(8, -4, 10);
                textInfo.text = "100% 请按任意键继续";
                slider.value = 1;
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }
            else
            {
                textInfo.text = string.Format("{0:f2}", (operation.progress * 100) + "%");
                slider.value = operation.progress;
            }

            yield return null;
        }
    }
}
