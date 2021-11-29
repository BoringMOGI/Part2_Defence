using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarUI : Singleton<TopBarUI>
{
    [SerializeField] Text hpText;
    [SerializeField] Text goldText;
    [SerializeField] Text waveText;
    [SerializeField] Text nextWaveText;

    const string nextWaveContext = "다음 라운드까지 {0:#,##0}초";

    public void SetLifeText(int hp)
    {
        hpText.text = hp.ToString();
    }
    public void SetGoldText(int gold)
    {
        goldText.text = gold.ToString("#,##0G"); // gold를 10,000처럼 3단위로 쉼표 구분.
    }

    public void SetWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }
    public void SetNextWaveTime(int nextTime)
    {
        nextWaveText.text = string.Format(nextWaveContext, nextTime);
    }
}
