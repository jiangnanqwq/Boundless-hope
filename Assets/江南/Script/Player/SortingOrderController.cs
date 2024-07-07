
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
        // 使用角色的Y轴位置来动态设置Order in Layer
        spriteRenderer.sortingOrder = (int)(-transform.position.y * 100);
        Debug.Log(spriteRenderer.sortingOrder);
    }
}
