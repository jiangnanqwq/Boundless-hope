using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotWalkTips : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(DelayActiveF());
        }
    }
    IEnumerator DelayActiveF()
    {
        BagManagement.instance.imageTips.SetActive(true);
        BagManagement.instance.imageTips.transform.GetChild(0).GetComponent<Text>().text = "前面的区域以后再来探索吧";
        yield return new WaitForSeconds(3);
        BagManagement.instance.imageTips.SetActive(false);
    }

}
