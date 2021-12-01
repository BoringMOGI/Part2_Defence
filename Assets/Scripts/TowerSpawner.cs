using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : Singleton<TowerSpawner>
{
    [System.Serializable]           // Ŭ����,����ü�� ����ȭ.
    public struct TowerPrefab
    {        
        public Tower.TOWER_TYPE type;
        public Tower[] towers;
    }

    [SerializeField] TowerPrefab[] towerPrefabs;
    [SerializeField] LayerMask tileLayer;

    GameManager gm;                         // ���� �Ŵ���.
    TileWall pointTile;                     // ���콺�� ���� �ִ� Ÿ��.
    Tower setupTower;                       // ������� Ÿ��.

    private void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        // ���콺 �����Ͱ� ��ġ�� ������ Ÿ���� �˻�.
        RayToTile();

        // ���� Ÿ���� �����Ǿ� ���� ������.
        if (setupTower == null)
        {
            if (pointTile == null)
                return;

            if (Input.GetMouseButtonDown(0))       // 0:���� ���콺 Ŭ��, 1:������, 2:��.
            {
                pointTile.OnSelectedTile();
            }
        }
        else   // ��� Ÿ���� ��ġ�Ϸ��� ��.
        {
            Vector3 towerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            towerPosition.z = 0f;
            setupTower.transform.position = towerPosition;      // Ÿ���� ��ġ�� ���콺 �������� ��ġ��.

            // ���콺�� Ÿ�� ���� �ְ� �ش� Ÿ�Ͽ� Ÿ���� ��ġ�Ǿ����� ���� ���.
            if (pointTile != null && !pointTile.IsOnTower)                          
            {
                setupTower.transform.position = pointTile.transform.position;       // �ش� Ÿ���� ��ġ�� Ÿ���� �ű��.

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

        // ���콺 �����Ͱ� layout �ȿ� ���� ���� ��� ��ġ ����.
        if (IsOnUI() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, tileLayer))
                pointTile = hit.collider.GetComponent<TileWall>();      // ������ ���� ��ü�� Ÿ�� ������Ʈ�� ����ִ°�?
        }
    }
    private bool IsOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();           // ���콺 �����Ͱ� UI���� �ִ°�?
    }

    // Ÿ�� ã��.
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

    // Ÿ�� ��ġ.
    public void SetupTower(Tower.TOWER_TYPE spawnType)
    {
        // �̹� ��ġ�Ϸ��� Ÿ���� ������ ��� return.
        if (setupTower != null)
            return;

        Tower prefab = GetTowerPrefab(spawnType, 1);
        setupTower = Instantiate(prefab, transform);      // ������ ������ 1���� Ÿ�� ����.
        setupTower.SwitchRange(true);
    }
}
