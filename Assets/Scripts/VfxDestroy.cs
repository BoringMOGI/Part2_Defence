using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxDestroy : MonoBehaviour
{
    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particle.isPlaying == false)
            Destroy(gameObject);
    }
}
