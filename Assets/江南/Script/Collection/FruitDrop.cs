
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FruitDrop : MonoBehaviour
{
    private float growTime = 300;//生长时间 单位秒
    private float timeCurrent = 0;

    private int fruitTotal = 2;//上限值
    private int fruitCount = 2;//当前可采摘的数量
    public int FruitCount
    {
        get => fruitCount;
        set
        {
            if (value <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                BagManagement.instance.collectionUI.SetActive(false);
            }
            else if (value < fruitCount)
            {
                BeginGrow();
            }
            fruitCount = value;
        }
    }
    private bool isGrow = false;

    private bool isPlayerInRange = false;
    private void Start()
    {
        growTime = 300; fruitTotal = 2; fruitCount = 2; isGrow = false; isPlayerInRange = false;
    }
    public void Init(float growTime, int fruitTotal, int fruitCount, bool isGrow)
    {
        if (fruitTotal <= 0)
        {
            Destroy(gameObject);
            return;
        }
        this.growTime = growTime;
        this.fruitCount = fruitCount;
        this.fruitTotal = fruitTotal;
        if (fruitCount <= 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(Grow());
            this.isGrow = true;
        }
        else if (isGrow)
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
        timeCurrent = 0;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BagManagement.instance.collectionUI.SetActive(true);

            if (fruitCount <= 0)
            {
                Image i = BagManagement.instance.collectionUI.GetComponent<Image>();
                i.sprite = Resources.Load<Sprite>("Image/Bush_Red");
                i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "生长中:"+ (int)timeCurrent+"/"+ (int)growTime;
                i.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ".";
                return;
            }

            Vector3 targetWorldPosition = collision.transform.position;
            targetWorldPosition.y += BagManagement.instance.collectionUIOffset;

            // 将世界空间坐标转换为屏幕空间坐标
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetWorldPosition);

            // 将屏幕空间坐标转换为UI元素的锚点位置
            BagManagement.instance.collectionUI.GetComponent<RectTransform>().position = screenPosition;

            Image image = BagManagement.instance.collectionUI.GetComponent<Image>();
            image.sprite = BagManagement.instance.objInfos.Find(x => x.ID == 5).sprite;
            image.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "野果";
            image.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "F";

            Button bt = image.GetComponent<Button>();
            bt.onClick.RemoveAllListeners();
            bt.onClick.AddListener(() => { BagManagement.instance.ObjToBag(5, GetFruit(1)); });
        }
    }
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && fruitCount > 0)
        {
            BagManagement.instance.ObjToBag(5, GetFruit(1));
            BagManagement.instance.ObjToBag(6, 1);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            BagManagement.instance.collectionUI.SetActive(false);
        }
    }
}
