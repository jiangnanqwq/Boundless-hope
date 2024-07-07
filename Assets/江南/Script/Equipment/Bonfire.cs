using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour
{
    private bool isFire = false;
    private bool isPlayerInRange;
    public Material material;
    //VoronoiPower
    public Sprite fire;
    public bool canCook = false;
    private Image image;
    //private float lightPercentage = 0;
    public Light2D ligthSpot;
    private void Start()
    {
        material.SetFloat("_VP", 10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BagManagement.instance.collectionUI.SetActive(true);

            Vector3 targetWorldPosition = collision.transform.position;
            targetWorldPosition.y += BagManagement.instance.collectionUIOffset;

            // ������ռ�����ת��Ϊ��Ļ�ռ�����
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetWorldPosition);

            // ����Ļ�ռ�����ת��ΪUIԪ�ص�ê��λ��
            BagManagement.instance.collectionUI.GetComponent<RectTransform>().position = screenPosition;

            image = BagManagement.instance.collectionUI.GetComponent<Image>();
            image.sprite = fire;
            if (!isFire)
            {
                image.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "���";
            }
            else
            {
                image.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "���";
            }
            image.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "F";

            Button bt = image.GetComponent<Button>();
            bt.onClick.RemoveAllListeners();
            bt.onClick.AddListener(OnBonfireBTClick);
        }
    }
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            OnBonfireBTClick();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (!isFire)
            {
                image.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "���";
            }
            else
            {
                image.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "���";
            }
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
    private void OnBonfireBTClick()
    {
        if (!isFire)
        {
            //���
            //voronoiPower��10����2
            StartCoroutine(ReduceFloat(material.GetFloat("_VP"), 2));
            //��ʾ ��������ק�����𼴿ɻ������
        }
        else
        {
            //���
            StartCoroutine(IncreaseFloat(material.GetFloat("_VP"), 10));
        }
        isFire = !isFire;
    }
    private IEnumerator ReduceFloat(float start, float to)//����
    {
        float s = start;
        float t = to;
        transform.GetChild(0).gameObject.SetActive(true);
        while (s >= t)
        {
            s -= Time.deltaTime * 3;
            material.SetFloat("_VP", s);

            ligthSpot.intensity = Mathf.Clamp01((10 - s) / 8);

            yield return null;
        }
        canCook = true;
        yield return ShowInformation();
    }
    private IEnumerator IncreaseFloat(float start, float to)
    {
        float s = start;
        float t = to;
        BagManagement.instance.imageTips.SetActive(false);
        canCook = false;
        while (s <= t)
        {
            s += Time.deltaTime * 3;
            material.SetFloat("_VP", s);

            ligthSpot.intensity = Mathf.Clamp01((10 - s) / 8);

            yield return null;
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private IEnumerator ShowInformation()
    {
        BagManagement.instance.imageTips.SetActive(true);
        BagManagement.instance.imageTips.transform.GetChild(0).GetComponent<Text>().text = "��ʳ����ק��������Ա������";
        yield return new WaitForSeconds(2);
        BagManagement.instance.imageTips.SetActive(false);
    }
}
