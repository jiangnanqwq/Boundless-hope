using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public int arrowsAmount = 10;
    public float playerHPMax = 100f;
    [SerializeField] private float playerHP = 100f;
    public float PlayerHP
    {
        get => playerHP;
        set
        {
            if (value <= 0)
            {
                //DeadAction
                return;
            }
            else if (value > playerHPMax)
            {
                value = playerHPMax;
            }
            playerHP = value;
            //更新UI
            BagManagement.instance.HPSlider.value = playerHP / playerHPMax;
            BagManagement.instance.HPText.text = (int)playerHP + "/" + (int)playerHPMax;
        }
    }
    public float playerSpeed = 2.5f;

    public float decreaseHPSpeed = 1;//每秒减少HP

    public DogAttribute dog;
    private void Update()
    {
        PlayerHP -= Time.deltaTime * decreaseHPSpeed;
    }

}
