using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ArrowsCtr : MonoBehaviour
{
    public float arrowSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
        Destroy(this.gameObject, 5f);

    }
    // Update is called once per frame
    void Update()
    {
        ArrowsMove();
    }

    void ArrowsMove()
    {
        this.transform.Translate(Vector3.up  * arrowSpeed * Time.deltaTime);
    } 
    private void OnTriggerEnter2D(Collider2D collision)//箭矢碰到物体带Animal tag的物体消灭彼此
    {
        Debug.Log("箭矢碰到了物体");
        if (collision.gameObject.tag == "Animal")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
