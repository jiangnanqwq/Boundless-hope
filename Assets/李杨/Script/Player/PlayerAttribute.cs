using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public static bool IsHidEnding1 = false;
    public static bool IsBeginHidEnding1Detect = false;
    public float playerHPMax = 100f;
    [SerializeField] private float playerHP = 100f;
    public float PlayerHP
    {
        get => playerHP;
        set
        {
            if (value <= 0)
            {
                gameObject.GetComponent<PlayerAction>().PlayerDie();
                return;
            }
            else if (value <= 5 && IsHidEnding1)
            {
                //���ؽ�� ���
                gameObject.GetComponent<PlayerAction>().PlayerHideEnding();
                return;
            }
            else if (value <= 20 && dog.trustMeCompletely && BagManagement.instance.objs.ContainsKey(7))//����С��10 ����ȫ���� �����������
            {
                BagManagement.instance.ShowTips("���ұ����Ѿõ���ɸ�������");
                IsBeginHidEnding1Detect=true;
                return;
            }
            else if (value > playerHPMax)
            {
                value = playerHPMax;
            }
            playerHP = value;
            //����UI
            BagManagement.instance.HPSlider.value = playerHP / playerHPMax;
            BagManagement.instance.HPText.text = (int)playerHP + "/" + (int)playerHPMax;
        }
    }
    public float playerSpeed = 2.5f;

    public float decreaseHPSpeed = 1;//ÿ�����HP

    public DogAttribute dog;
    private void Update()
    {
        PlayerHP -= Time.deltaTime * decreaseHPSpeed;
    }

}
