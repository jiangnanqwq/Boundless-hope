
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttribute : AttributeBase
{
    public bool isDead = false;
    public float DogHP
    {
        get => currentHP;
        set
        {
            if (value <= 0)
            {
                value = 0;
                if (!isDead)
                {
                    isDead = true;
                    agent.isStopped = true;
                    GetComponent<Collider2D>().enabled = false;
                    //DeadAction
                    GetComponent<Animator>().SetTrigger("Dead");
                    BagManagement.instance.ObjToBag(4, 1);
                    Destroy(gameObject, 20);
                }
            }
            else if (value >= MaxHP) value = MaxHP;
            currentHP = value;
            //UI
            BagManagement.instance.sliderLeftDog.fillAmount = currentHP / MaxHP;
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
            if (value <= 0)
            {
                value = 0;
                //No Trust Action

                BagManagement.instance.ShowTips("没爱了,毁灭吧[doge]");
            }
            else if (value >= dogTPMax)
            {
                value = dogTPMax;
                //Trust Action
                trustMeCompletely = true;

                BagManagement.instance.ShowTips("已经完全信任你,按C键辅助狩猎");

            }
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

    private readonly float decreaseHPSpeed = 0.5f;
    public bool trustMeCompletely = false;
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

        if (trustMeCompletely && MapManagement.Sheeps.Count > 0 && !isBiteInCD)
        //if (MapManagement.Sheeps.Count > 0 && !isBiteInCD)
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
        List<GameObject> sheeps = MapManagement.Sheeps;
        GameObject sheep = null;
        float d = float.MaxValue;
        Transform player = BagManagement.instance.playerAttribute.transform;
        for (int i = 0; i < sheeps.Count; ++i)
        {
            float d2 = Vector2.Distance(player.position, sheeps[i].transform.position);
            if (d2 < d)
            {
                sheep = sheeps[i];
                d = d2;
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
        if (obj == null || obj.dogEat.Length <= 0)
            return false;
        DogTP += obj.dogEat[0].trustPoint;
        DogHP += obj.hungerRecoverPoint;
        if (obj.ID == 7 && PlayerAttribute.IsBeginHidEnding1Detect)
            PlayerAttribute.IsHidEnding1 = true;
        return true;
    }
}
public enum AnimatorState
{
    idle, walk, attack
}