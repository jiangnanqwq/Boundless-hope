using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class KnifeCtr: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
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
    void KnifeAttack() {
        Animator animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("isCut");
    }
    void SwitchColliders()
    {
        if (gameObject.GetComponent<Collider2D>().enabled == false)
            gameObject.GetComponent<Collider2D>().enabled = true;
        else
            gameObject.GetComponent<Collider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("刀碰到了物体");
        if (collision.gameObject.tag == "Animal")
        {
            Destroy(collision.gameObject); 
        }
    }
}
