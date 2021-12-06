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
    [SerializeField] Transform poolParent;        // Ǯ���� ������Ʈ�� ����ִ� �θ� ������Ʈ.
    [SerializeField] int initEffectCount;

    Stack<AudioEffect> poolStorage;               // Ǯ�� ������Ʈ�� ����ִ� ���� ����.

    private void Start()
    {
        // ������ �ε�(Load)
        masterVolumn = PlayerPrefs.HasKey(KEY_MASTER) ? PlayerPrefs.GetFloat(KEY_MASTER) : 1f;
        bgmVolumn = PlayerPrefs.HasKey(KEY_BGM)       ? PlayerPrefs.GetFloat(KEY_BGM) : 1f;
        effectVolumn = PlayerPrefs.HasKey(KEY_EFFECT) ? PlayerPrefs.GetFloat(KEY_EFFECT) : 1f;

        bgmSource.volume = bgmVolumn * masterVolumn;        // ���� ����� ũ�� ����.

        poolStorage = new Stack<AudioEffect>();             // stack�� ��ü ����.
        CreatePool(initEffectCount);                        // ���� ���� ��ŭ ������Ʈ ����.
    }


    void CreatePool(int count = 1)
    {
        // count ����ŭ Ǯ�� ������Ʈ�� ���� �� Stack�� ����.
        for (int i = 0; i < count; i++)
        {
            AudioEffect newEffect = Instantiate(effectPrefab, poolParent);
            newEffect.Setup(OnReturnPool);
            poolStorage.Push(newEffect);
        }
    }
    AudioEffect GetPool()
    {
        if (poolStorage.Count <= 0)         // ����ҿ� Ǯ�� ������Ʈ�� ���ٸ�.
            CreatePool();                   // �ϳ� ����.

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
        AudioClip[] clips = (type == AUDIO.Bgm) ? bgms : effects;       // Ŭ�� �迭 ����.
        foreach(AudioClip clip in clips)
        {
            if (clip.name.Equals(str))                                  // Ŭ���� �̸��� ��û �̸��� ���ٸ�
                return clip;                                            // �ش� Ŭ�� ��ȯ.
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
