using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : Singleton<TowerSpawner>
{
    [System.Serializable]           // Ŭ����,����ü�� ����ȭ.
    public struct TowerPrefab
    {
        public Tower tower;
        public Tower.TOWER_TYPE type;
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

    public Tower GetCurrentTower()
    {
        // ������ �迭�� ��ȸ.
        foreach(TowerPrefab prefab in towerPrefabs)
        {
            if (prefab.type == spawnType)       // n��° ������ ������ type�� �����ϴٸ�.
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
