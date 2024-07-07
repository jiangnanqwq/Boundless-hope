
using System.Collections;
using UnityEngine;

public class DogAttribute : AttributeBase
{
    public float DogHP
    {
        get => currentHP;
        set
        {
            if (value < 0)
            {
                value = 0;
                //DeadAction


            }
            else if (value > MaxHP) value = MaxHP;
            currentHP = value;
            //UI
            BagManagement.instance.sliderLeftDog.fillAmount = currentHP/ MaxHP;
            BagManagement.instance.textLeftDog.text = (int)currentHP + "/" + (int)MaxHP;

        }
    }

    public float dogTPMax = 100;//Trust Point
    private float dogTP = 20;
    public float DogTP
    {
        get => dogTP;
        set
        {
            if (value < 0)
            {
                value = 0;
                //Trust Action


            }
            else if (value > dogTPMax) value = dogTPMax;
            dogTP = value;
            //UI
            BagManagement.instance.sliderRightDog.fillAmount = dogTP / dogTPMax;
            BagManagement.instance.textRightDog.text = (int)dogTP + "/" + (int)dogTPMax;
        }
    }

    [HideInInspector] public bool isBiteSheep = false;
    public float biteTime = 5;//咬住控制时间
    [HideInInspector] public bool isBiteInCD = false;
    public float biteCDTime = 5;//攻击CD时间

    private float decreaseHPSpeed = 0.5f;
    private void Start()
    {
        //speed = 5;
        biteTime = 5; 
        DogTP = 20;
        DogHP = MaxHP;
    }
    private void Update()
    {
        DogHP -= Time.deltaTime * decreaseHPSpeed;
    }







    GameObject sheep;
    public void Attack()
    {
        if (MapManagement.Sheeps.Length > 0 && !isBiteInCD && dogTP>=20)
        {
            isBiteInCD = true;
            sheep = FindNearestSheep();
            if (sheep != null)
            {
                StartCoroutine(FollowSheep(sheep.GetComponent<SheepAttribute>()));
                StartCoroutine(CheckPosition(sheep.transform));
            }
        }
    }
    public GameObject FindNearestSheep()
    {
        GameObject[] sheeps = MapManagement.Sheeps;
        GameObject sheep = null;
        float d = float.MaxValue;
        for (int i = 0; i < sheeps.Length; ++i)
        {
            if (Vector2.Distance(transform.position, sheeps[i].transform.position) < d)
            {
                sheep = sheeps[i];
            }
        }
        return sheep;
    }
    IEnumerator FollowSheep(SheepAttribute sheep)
    {
        while (!isBiteSheep)
        {
            SetTheDestination(sheep.transform.position);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator CheckPosition(Transform sheep)
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, sheep.position) < 0.5f)
            {
                isBiteSheep = true;
                sheep.GetComponent<SheepAttribute>().isBitedByDog = true;
                GetComponent<Animator>().SetTrigger("Attack");
                yield break;
            }
            yield return null;
        }
    }
    public void ResetAttackCD()
    {
        StartCoroutine(RACD());
    }
    IEnumerator RACD()
    {
        if (sheep != null)
        {
            sheep.GetComponent<SheepAttribute>().isBitedByDog = false;
        }
        float t = 0;
        while (t < biteCDTime)
        {
            t += Time.deltaTime;
            yield return null;
        }
        isBiteInCD = false;
    }
    public bool EatMeat(Object_Consume obj)
    {
        if(obj == null|| obj.dogEat == null)
            return false;
        DogTP += obj.dogEat.trustPoint;
        DogHP += obj.hungerRecoverPoint;
        return true;
    }
}
public enum AnimatorState
{
    idle, walk, attack
}