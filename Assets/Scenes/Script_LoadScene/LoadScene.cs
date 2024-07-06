using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Text textInfo;
    public Slider slider;

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
                textInfo.text = "100%";
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
