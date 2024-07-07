
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
            }
            else if (value > MaxHP) value = MaxHP;
            currentHP = value;
            //UI


        }
    }

    public float dogTPMax = 100;//Trust Point
    public float dogTP = 20;

    [HideInInspector] public bool isBiteSheep = false;
    public float biteTime = 5;//咬住控制时间
    [HideInInspector] public bool isBiteInCD = false;
    public float biteCDTime = 5;//攻击CD时间

    private void Start()
    {
        speed = 5;
        biteTime = 5;
    }
    GameObject sheep;
    public void Attack()
    {
        if (MapManagement.Sheeps.Length > 0 && !isBiteInCD)
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
        sheep.GetComponent<SheepAttribute>().isBitedByDog = false;
        float t = 0;
        while (t < biteCDTime)
        {
            t += Time.deltaTime;
            yield return null;
        }
        isBiteInCD = false;
    }
}
public enum AnimatorState
{
    idle, walk, attack
}