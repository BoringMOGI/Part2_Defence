using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : Bullet
{
    [SerializeField] float explodeRange;

    protected override void HitTarget()
    {
        // ���� �ݰ游ŭ ���� ���̸� �߻�.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRange);
        foreach(Collider2D collider in colliders)
        {
            // �ݸ����� enemy�� �´ٸ� �������� �ش�.
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                Debug.Log(power);
                enemy.OnDamaged(power);
            }
        }

        CreateVFX();
    }
}
