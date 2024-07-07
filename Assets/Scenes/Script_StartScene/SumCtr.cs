using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumCtr : MonoBehaviour
{
    public int endingIndex;
     private void Awake()
    {
        // 确保在场景切换时不会销毁此对象
        DontDestroyOnLoad(gameObject);
        endingIndex = 0;
    }
}
