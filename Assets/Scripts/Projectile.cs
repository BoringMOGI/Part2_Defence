using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ParticleSystem vfx;
    [SerializeField] float speed;   // �ӵ�.

    Enemy target;               // ��ǥ��.
    float power;                // ���ݷ�.

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

        // ���� ����� �Ÿ��� (����) ��������ٸ�.
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
