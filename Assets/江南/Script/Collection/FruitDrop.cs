using System.Collections;
using UnityEngine;

public class FruitDrop : MonoBehaviour
{
    public static int OccupyGrid = 2;
    public float growTime;
    public int fruitTotal;//上限值
    private int fruitCount;//当前可采摘的数量
    public int FruitCount
    {
        get => fruitCount;
        set
        {
            if (value <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (value < fruitCount)
            {
                BeginGrow();
            }
            fruitCount = value;
        }
    }
    private bool isGrow = false;
    public void Init(float growTime, int fruitTotal, int fruitCount, bool isGrow)
    {
        if (fruitTotal<=0)
        {
            Destroy(gameObject);
            return;
        }
        this.growTime = growTime;
        this.fruitCount = fruitCount;
        this.fruitTotal = fruitTotal;
        if (fruitCount <= 0 )
        {
            transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(Grow());
            this.isGrow = true;
        }
        else if(isGrow)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(Grow());
            this.isGrow = true;
        }
        else
        {
            this.isGrow = false;
        }
    }
    public void BeginGrow()
    {
        if (!isGrow)
            StartCoroutine(Grow());
    }
    private IEnumerator Grow()
    {
        isGrow = true;
        float timeCurrent = 0;
        while (timeCurrent < growTime)
        {
            yield return null;
            timeCurrent += Time.deltaTime;
        }
        transform.GetChild(0).gameObject.SetActive(true);
        FruitCount = fruitTotal;

        isGrow = false;
    }
    /// <param name="count">要获取的数量，可能会超过灌木丛拥有的数量</param>
    /// <returns>返回获取的数量</returns>
    public int GetFruit(int count)
    {
        if (count > fruitCount)
        {
            FruitCount = 0;
            return fruitCount;
        }
        FruitCount -= count;
        return count;
    }
}
