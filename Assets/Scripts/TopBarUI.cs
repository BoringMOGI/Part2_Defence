using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarUI : Singleton<TopBarUI>
{
    [SerializeField] Text hpText;
    [SerializeField] Text goldText;

    public void SetLifeText(int hp)
    {
        hpText.text = hp.ToString();
    }
    public void SetGoldText(int gold)
    {
        goldText.text = gold.ToString("#,##0G"); // gold�� 10,000ó�� 3������ ��ǥ ����.
    }
}
