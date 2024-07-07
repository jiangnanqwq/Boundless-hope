using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUIChange : MonoBehaviour
{
    public Image imageComponent; // �����Image�����ק���˴�
    public Sprite[] newSprites; // ��������Ҫ��������ͼƬ
    public Text tittleText;
    public Text buttonText;
    public Text jianjie;
    public Button quitButton;

    void Start()
    {
        ChangeSprite();
    }

    // ���������ĳ���ض��¼�����ʱ����ͼƬ�����Դ���һ��������
    public void ChangeSprite()
    {
        switch(GameObject.Find("GameCtr").GetComponent<SumCtr>().endingIndex)
        {
            case 0: imageComponent.sprite = newSprites[0];
                tittleText.text = "Hold On";
                buttonText.text = "��ʼ��Ϸ";
                quitButton.gameObject.SetActive(false);
                jianjie.text = "��Ϸ���\r\n  ��Ϸ������\r\n  \r\n  �������������⣬�㿪����С�ĵ�����ɽ�¡�\r\n  ����ɽ�²��ߣ�������İ�Ȯ�ܿ�������㣬����Ҳ�Զϵ�һ���ȵĴ��ۻ���������\r\n  \r\n  ��ϷĿ�꣺\r\n  \r\n  ����Ҫ�����ܵĻ���ȥ��\r\n  \r\n  ��Ϸ������\r\n  \r\n  ��/��/��/�ң�W/S/A/D\r\n  ����������������\r\n  �л�������shift\r\n  �������Ʒ��ק������-������������-�����������ӹ��������ȣ�\r\n  ���ӣ��������Ⱥ�׷���������C��\r\n  �������������������1-5\r\n  �������Ʒʹ�ã���ݼ�1-5�����������\r\n  ��������Ʒ������Ѫ�����⣬���⡢Ұ��\r\n  ��������Ʒ�����ӣ��������괦����Ұ���ӣ����������£��Ҽ�ȡ��\r\n  ������������ʾ������F�������\r\n  ����Ұ���ԣ�����ʾ������F�ɼ���һ��Ұ���Կɲɼ����Σ���һ�βɼ���Ұ���ԻῪʼ������180������±�����βɼ�\r\n  ���ף����ˡ�";
                break;
            case 1: imageComponent.sprite = newSprites[1];
                buttonText.text = "��������";
                tittleText.text = "Hope";
                quitButton.gameObject.SetActive(false);
                jianjie.text = "�ἺΪ���ˡ�����ֵ��������";
                break;
            case 2: imageComponent.sprite = newSprites[0];
                buttonText.text = "��ʼ��Ϸ";
                tittleText.text = "Help";
                quitButton.gameObject.SetActive(false);
                jianjie.text = "��ʱ����˽�Ļ���ȥȷʵ��һ�����������������ţ����ܻ��в�ͬ�Ľ�֡�";
                break;
        }
         
    }
}
