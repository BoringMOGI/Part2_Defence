using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem vfx;
    [SerializeField] float speed;           // 속도.

    protected Enemy target;                 // 목표물.
    protected float power;                  // 공격력.

    public void Setup(Enemy target, float power)
    {
        this.target = target;
        this.power = power;
    }


    void Update()
    {
        if (target == null)
        {
            OnCrush();
            return;
        }

        MoveTo();
    }


    private void MoveTo()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.DamagePivot, speed * Time.deltaTime);

        // 나와 상대의 거리가 (많이) 가까워졌다면.
        if (Vector2.Distance(transform.position, target.DamagePivot) <= float.Epsilon)
        {
            HitTarget();
            OnCrush();
        }
    }

    // abstract 함수 : 상속 받는 클래스는 해당 함수를 구현해야만 한다.
    protected abstract void HitTarget();

    protected void CreateVFX()
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
