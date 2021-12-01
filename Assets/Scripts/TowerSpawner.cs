using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : Singleton<TowerSpawner>
{
    [System.Serializable]           // 클래스,구조체의 직렬화.
    public struct TowerPrefab
    {        
        public Tower.TOWER_TYPE type;
        public Tower[] towers;
    }

    [SerializeField] TowerPrefab[] towerPrefabs;
    [SerializeField] LayerMask tileLayer;

    GameManager gm;                         // 게임 매니저.
    TileWall pointTile;                     // 마우스가 보고 있는 타일.
    Tower setupTower;                       // 만드려는 타워.

    private void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        // 마우스 포인터가 위치한 지점의 타일을 검색.
        RayToTile();

        // 스폰 타입이 지정되어 있지 않으면.
        if (setupTower == null)
        {
            if (pointTile == null)
                return;

            if (Input.GetMouseButtonDown(0))       // 0:왼쪽 마우스 클릭, 1:오른쪽, 2:휠.
            {
                pointTile.OnSelectedTile();
            }
        }
        else   // 어떠한 타워를 설치하려고 함.
        {
            Vector3 towerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            towerPosition.z = 0f;
            setupTower.transform.position = towerPosition;      // 타워의 위치는 마우스 포인터의 위치다.

            // 마우스가 타일 위에 있고 해당 타일에 타워가 설치되어있지 않을 경우.
            if (pointTile != null && !pointTile.IsOnTower)                          
            {
                setupTower.transform.position = pointTile.transform.position;       // 해당 타일의 위치에 타워를 옮긴다.

                if (Input.GetMouseButtonDown(0))
                {
                    pointTile.OnSetupTower(setupTower);
                    setupTower = null;
                }
            }
        }
    }

    private void RayToTile()
    {
        pointTile = null;

        // 마우스 포인터가 layout 안에 있지 않을 경우 설치 가능.
        if (IsOnUI() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, tileLayer))
                pointTile = hit.collider.GetComponent<TileWall>();      // 광선을 맞은 물체가 타일 컴포넌트를 들고있는가?
        }
    }
    private bool IsOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();           // 마우스 포인터가 UI위에 있는가?
    }

    // 타워 찾기.
    private Tower GetTowerPrefab(Tower.TOWER_TYPE type, int level)
    {
        foreach (TowerPrefab prefab in towerPrefabs)
        {
            if (prefab.type == type)
                return prefab.towers[level - 1];
        }
        return null;
    }
    public int TowerPrice(Tower.TOWER_TYPE type, int level)
    {
        if (level >= 3)
            return 0;

        Tower prefab = GetTowerPrefab(type, level);
        return prefab.TowerPrice;
    }

    // 타워 설치.
    public void SetupTower(Tower.TOWER_TYPE spawnType)
    {
        // 이미 설치하려는 타워가 생성된 경우 return.
        if (setupTower != null)
            return;

        Tower prefab = GetTowerPrefab(spawnType, 1);
        setupTower = Instantiate(prefab, transform);      // 프리팹 내부의 1레벨 타워 생성.
        setupTower.SwitchRange(true);
    }
}
