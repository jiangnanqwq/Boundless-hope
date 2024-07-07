using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUIChange : MonoBehaviour
{
    public Image imageComponent; // 将你的Image组件拖拽到此处
    public Sprite[] newSprites; // 这是你想要更换的新图片
    public Text tittleText;
    public Text buttonText;
    public Text jianjie;
    public Button quitButton;

    void Start()
    {
        ChangeSprite();
    }

    // 如果你想在某个特定事件发生时更改图片，可以创建一个方法：
    public void ChangeSprite()
    {
        switch(GameObject.Find("GameCtr").GetComponent<SumCtr>().endingIndex)
        {
            case 0: imageComponent.sprite = newSprites[0];
                tittleText.text = "Hold On";
                buttonText.text = "开始游戏";
                quitButton.gameObject.SetActive(false);
                jianjie.text = "游戏简介\r\n  游戏背景：\r\n  \r\n  由于你的疏忽大意，你开车不小心掉下了山崖。\r\n  所幸山崖不高，你新买的爱犬很快叫醒了你，而你也以断掉一条腿的代价活了下来。\r\n  \r\n  游戏目标：\r\n  \r\n  你需要尽可能的活下去。\r\n  \r\n  游戏操作：\r\n  \r\n  上/下/左/右：W/S/A/D\r\n  拉弓：鼠标左键攻击\r\n  切换武器：shift\r\n  快捷栏物品拖拽：生肉-篝火，生肉熟肉-狗（可以增加狗的信赖度）\r\n  狗子（满信赖度后）追击最近的羊：C键\r\n  快捷栏按键：键盘数字1-5\r\n  快捷栏物品使用：快捷键1-5或鼠标左键点击\r\n  消耗类物品：增加血量生肉，熟肉、野果\r\n  功能类物品：种子，点击在鼠标处生成野果从，左键地面放下，右键取消\r\n  靠近篝火：有提示，按下F点火生火。\r\n  靠近野果丛：有提示，按下F采集。一个野果丛可采集两次，第一次采集，野果丛会开始生长，180秒后重新变成两次采集\r\n  最后，祝你好运。";
                break;
            case 1: imageComponent.sprite = newSprites[1];
                buttonText.text = "继续游玩";
                tittleText.text = "Hope";
                quitButton.gameObject.SetActive(false);
                jianjie.text = "舍己为“人”的你值得这个结局";
                break;
            case 2: imageComponent.sprite = newSprites[0];
                buttonText.text = "开始游戏";
                tittleText.text = "Help";
                quitButton.gameObject.SetActive(false);
                jianjie.text = "有时候，自私的活下去确实是一种奢望，不妨先相信？可能会有不同的结局。";
                break;
        }
         
    }
}
