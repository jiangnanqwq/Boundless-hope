using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class KnifeCtr : MonoBehaviour
{
    HashSet<Collider2D> colliders;
    void Start()
    {
        colliders = new();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            KnifeAttack();
        }
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (worldMousePosition.x < this.transform.parent.parent.position.x)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (worldMousePosition.x > this.transform.parent.parent.position.x)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void KnifeAttack()
    {
        Animator animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("isCut");
        GameObject.Find("Player").GetComponent<PlayerSoundCtr>().PlayerCutAudio();
    }
    //动画事件
    //1:over
    //0
    void SwitchColliders(int i)
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, 2, LayerMask.GetMask("Entity"));
        
        if (collision && !colliders.Contains(collision))
        {
            colliders.Add(collision);
            if (collision.gameObject.CompareTag("Sheep"))
            {
                Debug.Log("刀碰到了羊");
                BagManagement.instance.ObjToBag(4, 2);

                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Dog"))
            {
                Debug.Log("刀碰到了狗");
                collision.GetComponent<DogAttribute>().DogHP -= 20;
            }
        }

        if (i == 1)
        {
            colliders.Clear();
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Sheep"))
    //    {
    //        Debug.Log("刀碰到了羊");
    //        BagManagement.instance.ObjToBag(4, 2);

    //        Destroy(collision.gameObject);
    //    }
    //    else if(collision.gameObject.CompareTag("Dog"))
    //    {
    //        Debug.Log("刀碰到了狗");
    //        collision.GetComponent<DogAttribute>().DogHP -= 20;
    //    }
    //}
}
