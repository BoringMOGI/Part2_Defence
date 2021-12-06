using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 해당 클래스가 존재하려면 AudioSource가 있어야한다.
[RequireComponent(typeof(AudioSource))]
public class AudioEffect : MonoBehaviour
{
    public delegate void OnReturnPoolEvent(AudioEffect effect);
    public event OnReturnPoolEvent OnReturnPool;

    AudioSource source;

    public void Setup(OnReturnPoolEvent OnReturnPool)
    {
        source = GetComponent<AudioSource>();
        this.OnReturnPool = OnReturnPool;
    }
    public void Play(AudioClip clip, float volumn)
    {
        source.clip = clip;
        source.volume = volumn;
        source.Play();

        StartCoroutine(Playing());
    }

    IEnumerator Playing()
    {
        while (source.isPlaying)
            yield return null;

        // 다시 오디오 매니저에게 되돌린다.
        // delegate이벤트를 타고 나 자신을 전달한다.
        OnReturnPool?.Invoke(this);
    }
}
