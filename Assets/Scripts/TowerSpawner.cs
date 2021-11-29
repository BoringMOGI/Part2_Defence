using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : Singleton<TowerSpawner>
{
    [System.Serializable]           // 클래스,구조체의 직렬화.
    public struct TowerPrefab
    {
        public Tower tower;
        public Tower.TOWER_TYPE type;
    }

    [SerializeField] TowerPrefab[] towerPrefabs;

    Tower.TOWER_TYPE spawnType;             // 스폰 타입.
    GameManager gm;                         // 게임 매니저.
    TileWall selectedTile;                  // 선택한 타일.

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
            // 마우스 포인터가 layout 안에 있지 않을 경우 설치 가능.
            if (IsOnUI() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    TileWall tile = hit.collider.GetComponent<TileWall>();      // 광선을 맞은 물체가 타일 컴포넌트를 들고있는가?
                    if(tile != null)
                        tile.OnSelectedTile();
                }
            }
        }
    }
    private bool IsOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();           // 마우스 포인터가 UI위에 있는가?
    }

    public Tower GetCurrentTower()
    {
        // 프리팹 배열을 순회.
        foreach(TowerPrefab prefab in towerPrefabs)
        {
            if (prefab.type == spawnType)       // n번째 프리팹 내부의 type이 동일하다면.
            {
                Tower newTower = Instantiate(prefab.tower, transform);
                return newTower;
            }
        }

        return null;
    }

    public void ChangeTowerType(Tower.TOWER_TYPE spawnType)
    {
        this.spawnType = spawnType;
    }
}
