using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle2D : MonoBehaviour
{
    public Light2D globalLight;
    public Gradient lightColor;
    public AnimationCurve lightIntensity;

    public float dayDuration = 120f; // 一天的长度，单位为秒
    public float currentTime = 0f;

    public static int Day=0;

    public DayCounter dayCounter;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= dayDuration)
        {
            currentTime = 0f;
            Day += 1;
            if (Day > 20)
            {
                gameObject.GetComponent<PlayerAction>().PlayerGoodEndint();
                GameObject.Find("GameCtr").GetComponent<SumCtr>().endingIndex = 2;
            }
            dayCounter.UpdateText();
        }

        float timePercent = currentTime / dayDuration;
        UpdateLighting(timePercent);
    }

    private void UpdateLighting(float timePercent)
    {
        globalLight.color = lightColor.Evaluate(timePercent);
        globalLight.intensity = lightIntensity.Evaluate(timePercent);

        dayCounter.UpdateImage(globalLight.color);
    }
}
