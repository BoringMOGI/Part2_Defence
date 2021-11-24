using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))       // 0:���� ���콺 Ŭ��, 1:������, 2:��.
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                TileWall tile = hit.collider.GetComponent<TileWall>();      // ������ ���� ��ü�� Ÿ�� ������Ʈ�� ����ִ°�?
                if(tile != null)
                {
                    SpawnTower(tile);
                }
            }
        }
    }

    private void SpawnTower(TileWall tile)
    {
        if (tile.IsOnTower == false)                                 // Ÿ�Ͽ� Ÿ���� ��ġ�Ǿ� ���� �ʴٸ�
        {
            Tower newTower = Instantiate(towerPrefab, transform);    // ������ ������Ʈ, �θ� Ʈ������.
            tile.SetTower(newTower);                                 // Ÿ�Ͽ� Ÿ�� ��ġ.
        }
    }
}
