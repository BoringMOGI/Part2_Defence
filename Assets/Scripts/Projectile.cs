using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ParticleSystem vfx;
    [SerializeField] float speed;   // 속도.

    Enemy target;               // 목표물.
    float power;                // 공격력.

    public void Setup(Enemy target, float power)
    {
        this.target = target;
        this.power = power;
    }


    void Update()
    {
        if(target == null)
        {
            OnCrush();
            return;
        }

        MoveTo();
    }


    private void MoveTo()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        // 나와 상대의 거리가 (많이) 가까워졌다면.
        if(Vector2.Distance(transform.position, target.transform.position) <= float.Epsilon)
        {
            HitTarget();
            OnCrush();
        }
    }

    private void HitTarget()
    {
        target.OnDamaged(power);
        CreateVFX();
    }
    private void CreateVFX()
    {
        //... vfx (visual effect)
        if (vfx == null)
            return;

        ParticleSystem particle = Instantiate(vfx, transform.position, Quaternion.identity);
        particle.Play();
    }

    private void OnCrush()
    {
        Destroy(gameObject);
    }
}
