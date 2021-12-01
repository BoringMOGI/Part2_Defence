using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem vfx;
    [SerializeField] float speed;           // �ӵ�.

    protected Enemy target;                 // ��ǥ��.
    protected float power;                  // ���ݷ�.

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

        // ���� ����� �Ÿ��� (����) ��������ٸ�.
        if (Vector2.Distance(transform.position, target.DamagePivot) <= float.Epsilon)
        {
            HitTarget();
            OnCrush();
        }
    }

    // abstract �Լ� : ��� �޴� Ŭ������ �ش� �Լ��� �����ؾ߸� �Ѵ�.
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
