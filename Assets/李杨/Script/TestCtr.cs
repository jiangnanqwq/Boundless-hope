using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestCtr : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("ToLoad");
    }
    IEnumerator ToLoad()
    {
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene("LoadScene");
    }
}
