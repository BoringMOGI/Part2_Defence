using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    // 자동 프로퍼티
    // = get(값의 참조)는 가능하지만 set(값의 대입)은 외부에서 불가능 하다.
    public static bool IsVibrate        { get; private set; }

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider effectSlider;
    [SerializeField] Toggle vibrateToggle;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;

        masterSlider.value  = audioManager.masterVolumn;
        bgmSlider.value     = audioManager.bgmVolumn;
        effectSlider.value  = audioManager.effectVolumn;
        vibrateToggle.isOn  = IsVibrate;

        //audioManager.PauseBGM();
    }

    public void OnChangedMasterSlider(Slider slider)
    {
        audioManager.OnChangedVolumn(AudioManager.AUDIO.Master, slider.value);
    }
    public void OnChangedBgmSlider(Slider slider)
    {
        audioManager.OnChangedVolumn(AudioManager.AUDIO.Bgm, slider.value);
    }
    public void OnChangedEffectSlider(Slider slider)
    {
        audioManager.OnChangedVolumn(AudioManager.AUDIO.Effect, slider.value);
    }

    public void OnToggleDown(Toggle toggle)
    {
        IsVibrate = toggle.isOn;
        //PlayerPrefs.SetInt(KEY_VIBRATE, toggle.isOn ? 1 : 0);
    }

    public void CloseOption()
    {
        audioManager.UnPuaseBGM();
        SceneManager.Instance.CloseOption();
    }
}
