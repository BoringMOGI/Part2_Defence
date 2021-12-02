using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] Image towerImage;
    [SerializeField] Image outline;
    [SerializeField] Text priceText;

    // static변수는 클래스에 속하는 변수로 1개만 존재한다.
    // 생성 시점은 최초로 사용될 때 한 번, 해제 시점은 프로그램의 종료.
    static List<TowerButton> allTowerButtons = new List<TowerButton>();
    Tower.TOWER_TYPE type;

    public static int Number = 10;

    // 게임 오브젝트가 Destroy되었을때 호출되는 이벤트 함수.
    private void OnDestroy()
    {
        Debug.Log("Destroy : " + name);
        allTowerButtons.Remove(this);
    }

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
        // 나 이외에 나머지 버튼의 아웃라인을 전부 끈다.
        foreach(TowerButton button in allTowerButtons)
            button.outline.enabled = (button.type == type);

        TowerSpawner.Instance.SetupTower(type);        // 타워 스포너의 스폰 타입을 변경한다.
    }

}
