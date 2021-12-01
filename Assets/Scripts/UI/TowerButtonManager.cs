using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonManager : MonoBehaviour
{
    [System.Serializable]
    public struct TowerData
    {
        public Sprite towerSprite;
        public Tower.TOWER_TYPE towerType;
        public int towerPrice;
    }

    [SerializeField] TowerButton buttonPrefab;
    [SerializeField] Transform layout;
    [SerializeField] TowerData[] towerDatas;

    private void Start()
    {
        foreach(TowerData data in towerDatas)
        {
            TowerButton newButton = Instantiate(buttonPrefab, layout);
            newButton.Setup(data.towerSprite, data.towerPrice, data.towerType);
        }
    }
}
