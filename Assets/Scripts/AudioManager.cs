using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public enum AUDIO
    {
        Master,
        Bgm,
        Effect,
    }

    public float masterVolumn { get; private set; }
    public float bgmVolumn    { get; private set; }
    public float effectVolumn { get; private set; }

    const string KEY_MASTER = "MasterVolumn";
    const string KEY_BGM    = "BgmVolumn";
    const string KEY_EFFECT = "EffectVolumn";

    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] effects;

    [Header("Source")]
    [SerializeField] AudioSource bgmSource;
    
    [Header("Pooling")]
    [SerializeField] AudioEffect effectPrefab;
    [SerializeField] Transform poolParent;        // 풀링할 오브젝트가 들어있는 부모 오브젝트.
    [SerializeField] int initEffectCount;

    Stack<AudioEffect> poolStorage;               // 풀링 오브젝트가 들어있는 스택 변수.

    private void Start()
    {
        // 데이터 로드(Load)
        masterVolumn = PlayerPrefs.HasKey(KEY_MASTER) ? PlayerPrefs.GetFloat(KEY_MASTER) : 1f;
        bgmVolumn = PlayerPrefs.HasKey(KEY_BGM)       ? PlayerPrefs.GetFloat(KEY_BGM) : 1f;
        effectVolumn = PlayerPrefs.HasKey(KEY_EFFECT) ? PlayerPrefs.GetFloat(KEY_EFFECT) : 1f;

        bgmSource.volume = bgmVolumn * masterVolumn;        // 최초 배경음 크기 설정.

        poolStorage = new Stack<AudioEffect>();             // stack에 객체 생성.
        CreatePool(initEffectCount);                        // 최초 개수 만큼 오브젝트 생성.
    }


    void CreatePool(int count = 1)
    {
        // count 수만큼 풀링 오브젝트를 생성 후 Stack에 대입.
        for (int i = 0; i < count; i++)
        {
            AudioEffect newEffect = Instantiate(effectPrefab, poolParent);
            newEffect.Setup(OnReturnPool);
            poolStorage.Push(newEffect);
        }
    }
    AudioEffect GetPool()
    {
        if (poolStorage.Count <= 0)         // 저장소에 풀링 오브젝트가 없다면.
            CreatePool();                   // 하나 생성.

        AudioEffect effect = poolStorage.Pop();
        effect.transform.SetParent(transform);
        return effect;
    }
    void OnReturnPool(AudioEffect effect)
    {
        effect.transform.SetParent(poolParent);
        poolStorage.Push(effect);
    }
    

    public void OnChangedVolumn(AUDIO type, float value)
    {
        switch(type)
        {
            case AUDIO.Master:
                masterVolumn = value;                
                PlayerPrefs.SetFloat(KEY_MASTER, masterVolumn);
                break;
            case AUDIO.Bgm:
                bgmVolumn = value;
                PlayerPrefs.SetFloat(KEY_BGM, bgmVolumn);
                break;
            case AUDIO.Effect:
                effectVolumn = value;
                PlayerPrefs.SetFloat(KEY_EFFECT, effectVolumn);
                break;
        }

        bgmSource.volume    = bgmVolumn * masterVolumn;
    }

    private AudioClip GetClip(AUDIO type, string str)
    {
        AudioClip[] clips = (type == AUDIO.Bgm) ? bgms : effects;       // 클립 배열 선택.
        foreach(AudioClip clip in clips)
        {
            if (clip.name.Equals(str))                                  // 클립의 이름이 요청 이름과 같다면
                return clip;                                            // 해당 클립 반환.
        }

        return null;
    }
    public void PlayBgm(string str)
    {
        bgmSource.clip = GetClip(AUDIO.Bgm, str);
        bgmSource.Play();
    }
    public void PauseBGM()
    {
        bgmSource.Pause();
    }
    public void UnPuaseBGM()
    {
        bgmSource.UnPause();
    }

    public void PlayEffect(string str)
    {
        AudioEffect effect = GetPool();
        effect.Play(GetClip(AUDIO.Effect, str), effectVolumn * masterVolumn);
    }
}
