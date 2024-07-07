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

    private bool isInBuild=false;//在建造中
    public void BuildPlant()
    {
        if (isInBuild) return;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 获取鼠标点击的世界坐标
                                                                                     
        mouseWorldPos.z = 0; // 确保Z轴为0，这样预制体会出现在摄像机前面
        plant = Instantiate(plantPrefab, mouseWorldPos, Quaternion.identity, plantContainer.transform); // 在该位置实例化植物
        plant.GetComponent<BoxCollider2D>().enabled = false;
        plant.transform.GetChild(0).gameObject.SetActive(false);
        plant.transform.GetChild(1).GetComponent<SpriteRenderer>().material = plantMaterial;
        StartCoroutine(PositionFollow(plant.transform));
        isInBuild = true;
    }
    IEnumerator PositionFollow(Transform plant)
    {
        while (true)
        {
            BagManagement.IsUIMouseLeftClick = true;

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // 确保Z轴为0，这样预制体会出现在摄像机前面
            plant.position = mouseWorldPos;

            if (Physics2D.OverlapBox(plant.position, new Vector2(2, 2), 0,LayerMask.GetMask("Barrier","Resource")))
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
                        plant.GetComponent<FruitDrop>().Init(360, 2, 0, true);
                        isInBuild = false;

                        yield return new WaitForEndOfFrame();
                        BagManagement.IsUIMouseLeftClick = false;

                        yield break;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(plant.gameObject);
                BagManagement.instance.ObjToBag(6, 1);
                isInBuild = false;
                BagManagement.IsUIMouseLeftClick = false;
                yield break;
            }

            yield return null;
        }
    }
}
