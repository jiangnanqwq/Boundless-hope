using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumCtr : MonoBehaviour
{
    public int endingIndex;
     private void Awake()
    {
        // ȷ���ڳ����л�ʱ�������ٴ˶���
        DontDestroyOnLoad(gameObject);
        endingIndex = 0;
    }
}
