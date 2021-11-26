using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : Singleton<TowerSpawner>
{
    [System.Serializable]           // 클래스,구조체의 직렬화.
    public struct TowerPrefab
    {
        public Tower tower;
        public Tower.TOWER_TYPE type;
    }

    [SerializeField] TowerPrefab[] towerPrefabs;

    Tower.TOWER_TYPE spawnType;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

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

    private Tower GetPrefab()
    {
        // 프리팹 배열을 순회.
        foreach(TowerPrefab prefab in towerPrefabs)
        {            
            if (prefab.type == spawnType)       // n번째 프리팹 내부의 type이 동일하다면.
                return prefab.tower;            // 해당 프리팹을 반환.
        }

        return null;
    }

    public void ChangeTowerType(Tower.TOWER_TYPE spawnType)
    {
        this.spawnType = spawnType;
    }
    private void SpawnTower(TileWall tile)
    {
        if (tile.IsOnTower)
            return;

        Tower prefab = GetPrefab();                             // 스폰할 타워의 프리팹을 대입. 
        if (gm.UseGold(prefab.TowerPrice))                      // 매니저에서 타워의 가격만큼의 골드를 소비했을 경우.
        {
            Tower newTower = Instantiate(prefab, transform);    // 생성할 오브젝트, 부모 트랜스폼.
            tile.SetTower(newTower);                            // 타일에 타워 설치.
        }
    }
}
