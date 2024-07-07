using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//��ʵ���Ǹ�״̬��
public class BagManagement : MonoBehaviour
{
    public static BagManagement instance;
    private void Awake()
    {
        instance = this;
    }

    public Image[] imageBackground;
    [Space] public Slider HPSlider;
    public Text HPText;
    [Space] public GameObject collectionUI;
    public float collectionUIOffset;

    /// <summary>
    /// 0ذ�� 1�� 2�� 3���� 4���� 5Ұ�� 6����
    /// </summary>
    public List<ObjectBase> objInfos;//���е���Ʒ��Ϣ
    /// <summary>
    /// Key:ID
    /// </summary>
    public Dictionary<int, ObjectOwn> objs;

    public PlayerAttribute playerAttribute;

    public PlantingSeed plantingSeed;

    public GameObject imageTips;

    public Image imageDog;
    public Image sliderLeftDog;
    public Image sliderRightDog;
    public Text textLeftDog;
    public Text textRightDog;

    private void Start()
    {
        UpdateUI();
        objInfos = new();
        objs = new();
        //LoadResourcesRecursively("��Ʒ����", objInfos);
        objInfos.AddRange(Resources.LoadAll<ObjectBase>("��Ʒ����"));
        ObjToBag(5, 2);
        ObjToBag(6, 2);
        ObjToBag(4, 5);
        playerAttribute =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttribute>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Slot s = imageBackground[0].GetComponentInChildren<Slot>();
            if (s != null) s.obj?.UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Slot s = imageBackground[1].GetComponentInChildren<Slot>();
            if (s != null) s.obj?.UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Slot s = imageBackground[2].GetComponentInChildren<Slot>();
            if (s != null) s.obj?.UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Slot s = imageBackground[3].GetComponentInChildren<Slot>();
            if (s != null) s.obj?.UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Slot s = imageBackground[4].GetComponentInChildren<Slot>();
            if (s != null) s.obj?.UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Slot s = imageBackground[5].GetComponentInChildren<Slot>();
            if (s != null) s.obj?.UseItem();
        }
    }


    /// <summary>
    /// ����Ƴ��������
    /// </summary>
    /// <param name="ID">��ƷID��Ψһ��0ذ�� 1�� 2�� 3���� 4���� 5Ұ�� 6����</param>
    /// <param name="count">��ֵ���  ��ֵ�Ƴ�</param>
    public void ObjToBag(int ID, int count)
    {
        if (count == 0) return;
        if (objs.ContainsKey(ID))
        {
            objs[ID].count += count;
            if (objs[ID].count <= 0)
            {
                objs.Remove(ID);
                if (ID == 2)//��
                    goto outSide;
                //��Ҫ�ڿ�����Ƴ�
                for (int i = 0; i < imageBackground.Length; ++i)
                {
                    Slot s = imageBackground[i].GetComponentInChildren<Slot>();
                    if (s != null && s.obj != null && s.obj.obj.ID == ID)
                    {
                        s.obj = null;
                        break;
                    }
                }
            }
        }
        else if (count > 0)
        {
            objs[ID] = new ObjectOwn(objInfos.Find(x => x.ID == ID), count);
            if (ID == 2)//��
                goto outSide;
            //��Ҫ�ڿ�������
            for (int i = 0; i < imageBackground.Length; ++i)
            {
                Slot s = imageBackground[i].GetComponentInChildren<Slot>(true);
                if (s != null && s.obj == null)
                {
                    s.obj = objs[ID];
                    break;
                }
            }
        }
    outSide:
        UpdateUI();
    }
    public void UpdateUI()
    {
        for (int i = 0; i < imageBackground.Length; ++i)
        {
            Slot s = imageBackground[i].GetComponentInChildren<Slot>(true);
            if (s.obj != null)
            {
                s.gameObject.SetActive(true);
                s.GetComponentInChildren<Text>().text = s.obj.count.ToString();
                s.GetComponent<Image>().sprite = s.obj.obj.sprite;
            }
            else
            {
                s.gameObject.SetActive(false);
            }
        }
    }
    //�������ť������
    public void OnGridClick()
    {
        Slot s = EventSystem.current.currentSelectedGameObject.GetComponent<Slot>();
        if (s != null) s.obj?.UseItem();
    }





    /// <summary>
    /// ��������
    /// </summary>
    public static void LoadResourcesRecursively<T>(string path, List<T> resources) where T : ObjectBase
    {
        T[] loadedResources = Resources.LoadAll<T>(path);

        string ss = ";";
        for (int i = 0; i < loadedResources.Length; ++i)
        {
            ss += loadedResources[i].objName + "  ";
        }
        Debug.Log(ss);

        resources.AddRange(loadedResources);

        // Assuming there are folders inside Resources folder that you want to search
        string[] directories = System.IO.Directory.GetDirectories(Application.dataPath + "/Resources/" + path);
        foreach (string directory in directories)
        {
            string directoryName = System.IO.Path.GetFileName(directory);
            Debug.Log(directory + ":" + directoryName);
            LoadResourcesRecursively(path + "/" + directoryName, resources);
        }
    }
}

public class ObjectOwn
{
    public ObjectBase obj;
    public int count;
    public ObjectOwn(ObjectBase objectBase, int count)
    {
        obj = objectBase;
        this.count = count;
    }
    public void UseItem()
    {
        switch (obj.category)
        {
            case ObjectCategory.Normal:
                if (obj.ID == 6)//����
                {
                    BagManagement.instance.plantingSeed.BuildPlant();
                }
                break;
            case ObjectCategory.Weapon:
                //if (obj.ID == 0)//ذ��
                //{
                //    //����
                //}
                //else if (obj.ID == 1)//��
                //{
                //    if (BagManagement.instance.objs.ContainsKey(2))//�������Ƿ��м�
                //    {
                //        //����
                //    }
                //}
                break;
            case ObjectCategory.Consume:
                //��Ӽ���ֵ
                if(BagManagement.instance.objs.ContainsKey(obj.ID)&& BagManagement.instance.objs[obj.ID].obj is Object_Consume objC)
                {
                    BagManagement.instance.playerAttribute.PlayerHP += objC.hungerRecoverPoint;
                }

                BagManagement.instance.ObjToBag(obj.ID, -1);
                break;
            default:
                break;
        }
    }
}
