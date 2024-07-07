using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantingSeed : MonoBehaviour
{
    public Tilemap soilTilemap; // 地面Tilemap
    public GameObject plantPrefab;
    public GameObject plantContainer; // 植物容器

    [Header("两个材质")]
    public Material plantMaterial;
    public Material plantNormal;
    public Color colerForbid;
    public Color colerAllow;
    private GameObject plant;
    public void BuildPlant()
    {
        if (plant != null) return;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 获取鼠标点击的世界坐标
                                                                                     //Vector3Int cellPosition = soilTilemap.WorldToCell(mouseWorldPos); // 将世界坐标转换为格子坐标
        mouseWorldPos.z = 0; // 确保Z轴为0，这样预制体会出现在摄像机前面
        plant = Instantiate(plantPrefab, mouseWorldPos, Quaternion.identity, plantContainer.transform); // 在该位置实例化植物
        plant.GetComponent<BoxCollider2D>().enabled = false;
        plant.transform.GetChild(0).gameObject.SetActive(false);
        plant.transform.GetChild(1).GetComponent<SpriteRenderer>().material = plantMaterial;
        StartCoroutine(PositionFollow(plant.transform));
        //if (!soilTilemap.HasTile(cellPosition)) // 检查该格子是否有Tile
        //{
        //    Vector3 plantPosition = soilTilemap.CellToWorld(cellPosition) + new Vector3(0.5f, 0.5f, 0); // 将格子坐标转换回世界坐标，并调整位置
        //    GameObject plant = Instantiate(plantPrefab, plantPosition, Quaternion.identity, plantContainer.transform); // 在该位置实例化植物
        //}
    }
    IEnumerator PositionFollow(Transform plant)
    {
        while (true)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // 确保Z轴为0，这样预制体会出现在摄像机前面
            plant.position = mouseWorldPos;

            if (Physics2D.OverlapBox(plant.position, new Vector2(2, 2), 0, 1 << 7))
            {
                plantMaterial.color = colerForbid;
            }
            else
            {
                plantMaterial.color = colerAllow;
                if (Input.GetMouseButtonDown(0))
                {
                    if (plant != null)
                    {
                        plant.GetComponent<BoxCollider2D>().enabled = true;
                        plant.GetComponent<FruitDrop>().Init(5, 5, 0, true);
                        yield break;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(plant.gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
