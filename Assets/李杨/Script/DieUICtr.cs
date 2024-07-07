using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieUICtr : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("ReturnStartScance");
        GameObject.Find("SoundController")?.GetComponent<SoundCtr>().PlayMusic(1);
    }
    IEnumerator ReturnStartScance()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartScene");
        GameObject.Find("SoundController")?.GetComponent<SoundCtr>().PlayMusic(0);
    }
}
