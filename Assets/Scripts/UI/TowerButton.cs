using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] Image towerImage;
    [SerializeField] Image outline;
    [SerializeField] Text priceText;

    static List<TowerButton> allTowerButtons = new List<TowerButton>();
    Tower.TOWER_TYPE type;

    public void Setup(Sprite towerSprite, int price, Tower.TOWER_TYPE type)
    {
        towerImage.sprite = towerSprite;
        priceText.text = price.ToString("#,##0G");
        outline.enabled = false;

        this.type = type;
        allTowerButtons.Add(this);
    }
    public void OnSelectedButton()
    {
        // �� �̿ܿ� ������ ��ư�� �ƿ������� ���� ����.
        foreach(TowerButton button in allTowerButtons)
            button.outline.enabled = (button.type == type);

        TowerSpawner.Instance.SetupTower(type);        // Ÿ�� �������� ���� Ÿ���� �����Ѵ�.
    }

}
