using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))       // 0:왼쪽 마우스 클릭, 1:오른쪽, 2:휠.
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                TileWall tile = hit.collider.GetComponent<TileWall>();      // 광선을 맞은 물체가 타일 컴포넌트를 들고있는가?
                if(tile != null)
                {
                    SpawnTower(tile);
                }
            }
        }
    }

    private void SpawnTower(TileWall tile)
    {
        if (tile.IsOnTower == false)                                 // 타일에 타워가 설치되어 있지 않다면
        {
            Tower newTower = Instantiate(towerPrefab, transform);    // 생성할 오브젝트, 부모 트랜스폼.
            tile.SetTower(newTower);                                 // 타일에 타워 설치.
        }
    }
}
