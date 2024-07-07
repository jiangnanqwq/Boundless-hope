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
        if (collision.gameObject.CompareTag("Sheep"))
        {
            Debug.Log("箭矢碰到了羊");
            BagManagement.instance.ObjToBag(4, 2);

            MapManagement.Sheeps.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Dog"))
        {
            Debug.Log("箭矢碰到了狗");
            collision.GetComponent<DogAttribute>().DogHP -= 20;
            collision.GetComponent<DogAttribute>().DogTP -= 20;
        }
    }
}
