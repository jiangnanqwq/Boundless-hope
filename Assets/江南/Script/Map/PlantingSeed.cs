using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantingSeed : MonoBehaviour
{
    public Tilemap soilTilemap; // ����Tilemap
    public GameObject plantPrefab;
    public GameObject plantContainer; // ֲ������

    [Header("��������")]
    public Material plantMaterial;
    public Material plantNormal;
    public Color colerForbid;
    public Color colerAllow;
    private GameObject plant;
    public void BuildPlant()
    {
        if (plant != null) return;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ��ȡ���������������
                                                                                     //Vector3Int cellPosition = soilTilemap.WorldToCell(mouseWorldPos); // ����������ת��Ϊ��������
        mouseWorldPos.z = 0; // ȷ��Z��Ϊ0������Ԥ���������������ǰ��
        plant = Instantiate(plantPrefab, mouseWorldPos, Quaternion.identity, plantContainer.transform); // �ڸ�λ��ʵ����ֲ��
        plant.GetComponent<BoxCollider2D>().enabled = false;
        plant.transform.GetChild(0).gameObject.SetActive(false);
        plant.transform.GetChild(1).GetComponent<SpriteRenderer>().material = plantMaterial;
        StartCoroutine(PositionFollow(plant.transform));
        //if (!soilTilemap.HasTile(cellPosition)) // ���ø����Ƿ���Tile
        //{
        //    Vector3 plantPosition = soilTilemap.CellToWorld(cellPosition) + new Vector3(0.5f, 0.5f, 0); // ����������ת�����������꣬������λ��
        //    GameObject plant = Instantiate(plantPrefab, plantPosition, Quaternion.identity, plantContainer.transform); // �ڸ�λ��ʵ����ֲ��
        //}
    }
    IEnumerator PositionFollow(Transform plant)
    {
        while (true)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // ȷ��Z��Ϊ0������Ԥ���������������ǰ��
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
