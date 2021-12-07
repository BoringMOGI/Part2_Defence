using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Mixer : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            float bgm = 0f;
            if(mixer.GetFloat("bgm", out bgm))
            {
                bgm += 1f;
                mixer.SetFloat("bgm", bgm);
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            float bgm = 0f;
            if(mixer.GetFloat("bgm", out bgm))
            {
                bgm -= 1;
                mixer.SetFloat("bgm", bgm);
            }
        }

    }

    [SerializeField] float minVolume;

    public void OnChangedVolumn(Slider slider)
    {
        float ratio = slider.value;                         // �����̴��� ����.
        float volume = -minVolume + (minVolume * ratio);    // ������ ���� ���� ����.
        if (volume <= -minVolume)                           // �ּ� �������� �����Դٸ�.
            volume = -80f;                                  // ���Ұ�.

        mixer.SetFloat("bgm", volume);
    }
}
