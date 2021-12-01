using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWall : MonoBehaviour
{
    private Tower onTower;                          // 위에 있는 타워.
    public bool IsOnTower => onTower != null;       // 타워가 설치되어 있는가?

    static System.Action OnDisableAllFrame;         // 모든 타일의 프레임을 끄는 이벤트.

    [SerializeField] GameObject selectFrame;


    private void Start()
    {
        // 람다식을 이용해 델리게이트에 함수 등록.
        OnDisableAllFrame += () => { SwitchFrame(false); };
        SwitchFrame(false);
    }

    bool isSelected;
    private void SwitchFrame(bool isShow)
    {
        selectFrame.SetActive(isShow);
        isSelected = isShow;
    }

    // 타일 위에 마우스 포인터 위치.
    private void OnMouseOver()
    {
        if (onTower == null)
            return;

        onTower.SwitchRange(true);
    }
    private void OnMouseExit()
    {
        if (onTower == null)
            return;

        onTower.SwitchRange(false);
    }
    // 타일을 선택했다.
    public void OnSelectedTile()
    {
        if (IsOnTower)
        {
            OnDisableAllFrame?.Invoke();                                    // 나머지 타일 프레임 끄기.
            SwitchFrame(true);                                              // 나의 타일 프레임 켜기.

            TowerTool.Instance.OpenTool(onTower, OnSelectedTowerTool);      // Tool UI 출력.
        }
    }

    public void OnSetupTower(Tower newTower)
    {
        if (newTower == null)
            return;

        if (onTower)
            Destroy(onTower.gameObject);                                 // 기존에 설치된 타워를 제거.

        newTower.transform.position = transform.position;                // 해당 타워의 포지션을 나에게 맞춘다.
        newTower.OnSetupTower();                                         // 해당 타워가 설치되었음을 알림.
        onTower = newTower;                                              // 내가 가지고 있는 타워에 대입.
    }
    private void OnSelectedTowerTool(TowerTool.TOOL_TYPE type)
    {
        switch(type)
        {
            case TowerTool.TOOL_TYPE.Close:
                break;

            case TowerTool.TOOL_TYPE.Upgrade:
                /*
                if(onTower.TowerLevel < 3)
                {
                    Tower nextLevelTower = TowerSpawner.Instance.GetTowerObject(onTower.Type, onTower.TowerLevel + 1);
                    OnSetupTower(nextLevelTower);
                }
                */
                break;

            case TowerTool.TOOL_TYPE.Sell:
                if(onTower != null)
                {
                    int getGold = onTower.SellPrice;                    // 설치된 타워의 가격.
                    GameManager.Instance.OnGetGold(getGold);            // 해당 가격만큼 GM에게 전달.
                    Destroy(onTower.gameObject);                        // 설치된 타워 제거.
                }
                break;
        }

        OnDisableAllFrame?.Invoke();                                    // 나머지 타일 프레임 끄기.
    }
    
}
