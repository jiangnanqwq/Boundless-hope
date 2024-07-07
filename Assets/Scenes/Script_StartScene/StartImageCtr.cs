using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartImageCtr : MonoBehaviour
{
    public Image imageComponent; // �����Image�����ק���˴�
    public Sprite[] newSprites; // ��������Ҫ��������ͼƬ 
    public Text tittleText;
    public Text buttonText;
    public Button quitButton;
    void Start()
    {
        if (imageComponent == null || newSprites == null)
        {
            Debug.LogError("Image component or new sprite is not set.");
            return;
        }
        ChangedStartUI();
    }
    private void ChangedStartUI()
    { 
        switch(GameObject.Find("GameCtr").GetComponent<SumCtr>().endingIndex)
        {
            case 0: imageComponent.sprite = newSprites[0];
                tittleText.text = "Hold On";
                buttonText.text = "��ʼ��Ϸ";
                quitButton.gameObject.SetActive(false);
                break;
            case 1: imageComponent.sprite = newSprites[1];
                tittleText.text = "Hope";
                buttonText.text = "��Ҫ����?";
                quitButton.gameObject.SetActive(true);
                break;
            case 2: imageComponent.sprite = newSprites[0];
                tittleText.text = "Help";
                quitButton.gameObject.SetActive(true);
                break;
        }
    }

}
