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

    private void OnMouseEnter()             // 마우스 포인터가 들어왔을 때.
    {
        OnDisableAllFrame?.Invoke();
        SwitchFrame(true);
    }
    private void OnMouseExit()              // 마우스 포인터가 나갔을 때.
    {
        SwitchFrame(false);
    }
    private void OnMouseUpAsButton()        // 마우스를 클릭했을 때.
    {
        
    }

    private void SwitchFrame(bool isShow)
    {
        selectFrame.SetActive(isShow);
        isSelected = isShow;
    }

    public void OnSelectedTile()
    {
        if(IsOnTower)
        {
            OnDisableAllFrame?.Invoke();                                    // 나머지 타일 프레임 끄기.
            SwitchFrame(true);                                              // 나의 타일 프레임 켜기.

            TowerTool.Instance.OpenTool(transform);                         // Tool UI 출력.
        }
        else
        {
            Tower newTower = TowerSpawner.Instance.GetCurrentTower();       // 타워 스포너에게 새로운 타워를 받아온다.
            newTower.transform.position = transform.position;               // 해당 타워의 포지션을 나에게 맞춘다.
            onTower = newTower;                                             // 내가 가지고 있는 타워에 대입.
        }
    }

    
}
