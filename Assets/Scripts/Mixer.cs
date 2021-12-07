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
        float ratio = slider.value;                         // 슬라이더의 비율.
        float volume = -minVolume + (minVolume * ratio);    // 비율에 따른 볼륨 조절.
        if (volume <= -minVolume)                           // 최소 볼륨까지 내려왔다면.
            volume = -80f;                                  // 음소거.

        mixer.SetFloat("bgm", volume);
    }
}
