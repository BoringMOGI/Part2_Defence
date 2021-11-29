using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TOWER_TYPE
    {
        Projectile,        // 일반.
        Projectile2,
        Projectile3,
        Expolde,           // 폭발.
        Expolde2,
        Expolde3,
    }

    [SerializeField] TOWER_TYPE type;
    [SerializeField] Bullet bulletPrefab;     // 총알.
    [SerializeField] float attackRange;       // 공격 범위.
    [SerializeField] float attackRate;        // 공격 속도.
    [SerializeField] float attackPower;       // 공격력.
    [SerializeField] int towerPrice;          // 타워의 가격.

    public int TowerPrice => towerPrice;

    float nextAttackTime = 0.0f;
    Enemy enemy;

    private void Update()
    {
        Search();                             // 적을 탐색.

        if (enemy != null)
        {
            LookTarget();
            Fire();
        }
    }
    private void Search()
    {
        // 1.적이 없거나 공격 범위에서 벗어나면 새로운 적을 탐색한다.
        // 2.탐색 우선 순위는 가장 가까운 거리의 적이다.
        if(enemy == null || Vector2.Distance(transform.position, enemy.transform.position) > attackRange)
        {
            // 새로운 적 탐색.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
            float minDistance = attackRange;

            // 모든 콜리더를 순회.
            foreach (Collider2D collider in colliders)
            {
                Enemy target = collider.GetComponent<Enemy>();
                if (target != null)
                {
                    // 적과의 거리 측정 후 가장 최소 거리의 적을 대입한다.
                    float distance = Vector2.Distance(transform.position, target.transform.position);   // 나와 대상의 거리.
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        enemy = target;
                    }
                }
            }
        }
    }

    private void LookTarget()
    {
        Vector3 dir = enemy.transform.position - transform.position;
        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion lookAt = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.rotation = lookAt;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, 10 * Time.deltaTime);
    }
    private void Fire()
    {
        if (nextAttackTime <= Time.time)                    // 공격 시간이 되었다면.
        {
            nextAttackTime = Time.time + attackRate;        // 공격 시간 갱신.            
            if (enemy != null)                              // 적이 탐색되었다면.
            {
                Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.Setup(enemy, attackPower);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
