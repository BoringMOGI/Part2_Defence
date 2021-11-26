using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : Singleton<TowerSpawner>
{
    [System.Serializable]           // Ŭ����,����ü�� ����ȭ.
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

    private Tower GetPrefab()
    {
        // ������ �迭�� ��ȸ.
        foreach(TowerPrefab prefab in towerPrefabs)
        {            
            if (prefab.type == spawnType)       // n��° ������ ������ type�� �����ϴٸ�.
                return prefab.tower;            // �ش� �������� ��ȯ.
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

        Tower prefab = GetPrefab();                             // ������ Ÿ���� �������� ����. 
        if (gm.UseGold(prefab.TowerPrice))                      // �Ŵ������� Ÿ���� ���ݸ�ŭ�� ��带 �Һ����� ���.
        {
            Tower newTower = Instantiate(prefab, transform);    // ������ ������Ʈ, �θ� Ʈ������.
            tile.SetTower(newTower);                            // Ÿ�Ͽ� Ÿ�� ��ġ.
        }
    }
}
