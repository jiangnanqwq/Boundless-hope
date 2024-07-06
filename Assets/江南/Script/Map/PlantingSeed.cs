using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantingSeed : MonoBehaviour
{
    public Tilemap soilTilemap; // 地面Tilemap
    public GameObject plantPrefab;
    public GameObject plantContainer; // 植物容器
    public Material plantMaterial;
    public Color colerForbid;
    public Color colerAllow;
    private GameObject plant;
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 左键点击
        {
            if (plant != null) return;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 获取鼠标点击的世界坐标
            //Vector3Int cellPosition = soilTilemap.WorldToCell(mouseWorldPos); // 将世界坐标转换为格子坐标
            mouseWorldPos.z = 0; // 确保Z轴为0，这样预制体会出现在摄像机前面
            plant = Instantiate(plantPrefab, mouseWorldPos, Quaternion.identity, plantContainer.transform); // 在该位置实例化植物
            plant.transform.GetChild(0).gameObject.SetActive(false);
            plant.transform.GetChild(1).GetComponent<SpriteRenderer>().material = plantMaterial;
            StartCoroutine(PositionFollow(plant.transform));
            StartCoroutine(CheckBuild(plant.transform));
            //Debug.Log(cellPosition);
            //if (!soilTilemap.HasTile(cellPosition)) // 检查该格子是否有Tile
            //{
            //    Vector3 plantPosition = soilTilemap.CellToWorld(cellPosition) + new Vector3(0.5f, 0.5f, 0); // 将格子坐标转换回世界坐标，并调整位置
            //    GameObject plant = Instantiate(plantPrefab, plantPosition, Quaternion.identity, plantContainer.transform); // 在该位置实例化植物
            //}
        }
        else if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            if (plant != null)
            {
                plant.GetComponent<FruitDrop>().Init(5, 5, 0, true);
                plant = null;
            }
        }
    }
    IEnumerator PositionFollow(Transform plant)
    {
        while (true)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // 确保Z轴为0，这样预制体会出现在摄像机前面
            plant.position = mouseWorldPos;

            yield return null;
        }
    }
    IEnumerator CheckBuild(Transform plant)
    {
        while (true)
        {
            if (Physics2D.OverlapBox(plant.position, new Vector2(2, 2), 0, 1<<7))
            {
                plantMaterial.color = colerForbid;
            }
            else
            {
                plantMaterial.color = colerAllow;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
