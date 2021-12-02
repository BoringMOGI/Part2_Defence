using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarUI : Singleton<HpBarUI>
{
    [SerializeField] HpBar prefab;
    
    public void SetHpBar(IHealth target)
    {
        HpBar hpBar = Instantiate(prefab, transform);       // 프리팹을 클론 생성.
        hpBar.Setup(target);                                // 해당 프리팹 세팅.
    }
}
