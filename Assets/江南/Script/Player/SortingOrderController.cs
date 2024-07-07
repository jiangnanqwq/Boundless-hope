
using UnityEngine;

public class SortingOrderController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ʹ�ý�ɫ��Y��λ������̬����Order in Layer
        spriteRenderer.sortingOrder = (int)(-transform.position.y * 100);
        Debug.Log(spriteRenderer.sortingOrder);
    }
}
