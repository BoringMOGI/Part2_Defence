using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ش� Ŭ������ �����Ϸ��� AudioSource�� �־���Ѵ�.
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

        // �ٽ� ����� �Ŵ������� �ǵ�����.
        // delegate�̺�Ʈ�� Ÿ�� �� �ڽ��� �����Ѵ�.
        OnReturnPool?.Invoke(this);
    }
}
