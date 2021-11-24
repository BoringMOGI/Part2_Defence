using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform bulletPrefab;  // 총알.
    [SerializeField] float attackRange;       // 공격 범위.
    [SerializeField] float attackRate;        // 공격 속도.
    [SerializeField] float attackPower;       // 공격력.

    float nextAttackTime = 0.0f;

    private void Update()
    {
        if(nextAttackTime <= Time.time)                     // 공격 시간이 되었다면.
        {
            nextAttackTime = Time.time + attackRate;        // 공격 시간 갱신.
            Enemy enemy = Target();                         // 적을 탐색.
            if (enemy != null)                              // 적이 탐색되었다면.
            {
                Fire(enemy);
            }
        }
    }
    private Enemy Target()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        Enemy enemy = null;
        float maxDistance = 0.0f;

        Debug.Log(colliders.Length);

        foreach(Collider2D collider in colliders)
        {
            Enemy target = collider.GetComponent<Enemy>();
            if(target != null)
            {
                float distance = Vector2.Distance(transform.position, target.transform.position);   // 나와 대상의 거리.
                if(maxDistance < distance)
                {
                    enemy = target;
                    maxDistance = distance;
                }
            }
        }

        return enemy;
    }
    private void Fire(Enemy enemy)
    {
        Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
