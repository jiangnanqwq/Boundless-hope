
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FruitDrop : MonoBehaviour
{
    public static int OccupyGrid = 2;
    public float growTime;
    public int fruitTotal;//����ֵ
    private int fruitCount;//��ǰ�ɲ�ժ������
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
        fruitCount = fruitTotal;
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
    /// <param name="count">Ҫ��ȡ�����������ܻᳬ����ľ��ӵ�е�����</param>
    /// <returns>���ػ�ȡ������</returns>
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
            if (fruitCount <= 0) return;
            BagManagement.instance.collectionUI.SetActive(true);

            Vector3 targetWorldPosition = collision.transform.position;
            targetWorldPosition.y += BagManagement.instance.collectionUIOffset;

            // ������ռ�����ת��Ϊ��Ļ�ռ�����
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetWorldPosition);

            // ����Ļ�ռ�����ת��ΪUIԪ�ص�ê��λ��
            BagManagement.instance.collectionUI.GetComponent<RectTransform>().position = screenPosition;

            Image image = BagManagement.instance.collectionUI.GetComponent<Image>();
            image.sprite = BagManagement.instance.objInfos.Find(x => x.ID == 5).sprite;
            image.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Ұ��";
            image.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "F";

            Button bt = image.GetComponent<Button>();
            bt.onClick.RemoveAllListeners();
            bt.onClick.AddListener(() => { BagManagement.instance.ObjToBag(5, GetFruit(1)); });
        }
    }
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            BagManagement.instance.ObjToBag(5, GetFruit(1));
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
