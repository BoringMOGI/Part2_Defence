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

    const string nextWaveContext = "���� ������� {0:#,##0}��";

    public void SetLifeText(int hp)
    {
        hpText.text = hp.ToString();
    }
    public void SetGoldText(int gold)
    {
        goldText.text = gold.ToString("#,##0G"); // gold�� 10,000ó�� 3������ ��ǥ ����.
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
