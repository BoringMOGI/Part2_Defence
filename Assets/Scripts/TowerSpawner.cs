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

    Tower.TOWER_TYPE spawnType;             // ���� Ÿ��.
    GameManager gm;                         // ���� �Ŵ���.
    TileWall selectedTile;                  // ������ Ÿ��.

    private void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        if(Input.GetMouseButtonDown(0))       // 0:���� ���콺 Ŭ��, 1:������, 2:��.
        {
            // ���콺 �����Ͱ� layout �ȿ� ���� ���� ��� ��ġ ����.
            if (IsOnUI() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    TileWall tile = hit.collider.GetComponent<TileWall>();      // ������ ���� ��ü�� Ÿ�� ������Ʈ�� ����ִ°�?
                    if(tile != null)
                        tile.OnSelectedTile();
                }
            }
        }
    }
    private bool IsOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();           // ���콺 �����Ͱ� UI���� �ִ°�?
    }

    public Tower GetSelectedTower()
    {
        return GetTowerObject(spawnType, 1);
    }
    public Tower GetTowerObject(Tower.TOWER_TYPE type, int level)
    {
        Tower towerPrefab = GetTowerPrefab(type, level);

        // ��Ȯ�� �������� ã�Ұ� ���� ���� �������� ����� ���.
        if (towerPrefab != null && gm.UseGold(towerPrefab.TowerPrice))
        {
            return Instantiate(towerPrefab);
        }
        else
        {
            return null;
        }
    }

    // Ÿ�� ������ ã��.
    private Tower GetTowerPrefab(Tower.TOWER_TYPE type, int level)
    {
        Tower towerPrefab = null;

        // towerPrefabs ���ο��� ���ϴ� type�� level�� ���� Ÿ�� �������� Ž��.
        foreach (TowerPrefab prefab in towerPrefabs)
        {
            if (prefab.type == type)
            {
                towerPrefab = prefab.towers[level - 1];
                break;
            }
        }

        return towerPrefab;
    }

    public int TowerPrice(Tower.TOWER_TYPE type, int level)
    {
        if (level >= 3)
            return 0;

        Tower prefab = GetTowerPrefab(type, level);
        return prefab.TowerPrice;
    }

    public void ChangeTowerType(Tower.TOWER_TYPE spawnType)
    {
        this.spawnType = spawnType;
    }
}
