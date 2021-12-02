using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarUI : Singleton<HpBarUI>
{
    [SerializeField] HpBar prefab;
    
    public void SetHpBar(IHealth target)
    {
        HpBar hpBar = Instantiate(prefab, transform);       // �������� Ŭ�� ����.
        hpBar.Setup(target);                                // �ش� ������ ����.
    }
}
