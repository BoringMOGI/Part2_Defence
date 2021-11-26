using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : Bullet
{
    [SerializeField] float explodeRange;

    protected override void HitTarget()
    {
        // 폭발 반경만큼 원형 레이를 발사.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRange);
        foreach(Collider2D collider in colliders)
        {
            // 콜리더가 enemy가 맞다면 데미지를 준다.
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
