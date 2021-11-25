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
        goldText.text = gold.ToString("#,##0G"); // gold를 10,000처럼 3단위로 쉼표 구분.
    }
}
