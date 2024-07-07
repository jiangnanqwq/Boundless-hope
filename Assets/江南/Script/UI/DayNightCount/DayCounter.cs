using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour
{
    Image image;
    Text text;
    private void Start()
    {
        image= GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();
    }
    public void UpdateText()
    {
        text.text = "ÌìÊý:" + DayNightCycle2D.Day;
    }
    public void UpdateImage(Color color)
    {
        image.color = color;
    }
}
