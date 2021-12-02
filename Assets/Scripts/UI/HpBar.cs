using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// hp : hit point.
// hp : health point.
public interface IHealth
{
    bool IsAlive();
    float GetCurrentHP();
    float GetMaxHp();
    Vector3 GetPosition();
}

public class HpBar : MonoBehaviour
{
    [SerializeField] Image hpImage;


    IHealth target;

    public void Setup(IHealth target)           // 체력바 세팅.
    {
        this.target = target;                   // 인터페이스 대입.
    }

    private void Update()
    {
        if (target.IsAlive())
        {
            hpImage.fillAmount = target.GetCurrentHP() / target.GetMaxHp();             // 체력바의 fillAmount 갱신.
            transform.position = Camera.main.WorldToScreenPoint(target.GetPosition());  // 타겟의 위치(월드 좌표) -> UI의 위치(스크린 좌표)
        }
        else
            Destroy(gameObject);                                                        // 타겟이 사라졌을 때 내 오브젝트 제거.
    }
}
