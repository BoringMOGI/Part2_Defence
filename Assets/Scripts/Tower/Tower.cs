using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TOWER_TYPE
    {
        Projectile,        // 일반.
        Expolde,           // 폭발.
    }

    [SerializeField] TOWER_TYPE type;
    [SerializeField] Sprite towerSprite;      // 타워의 이미지.
    [SerializeField] Bullet bulletPrefab;     // 총알.
    [SerializeField] float attackRange;       // 공격 범위.
    [SerializeField] float attackRate;        // 공격 속도.
    [SerializeField] float attackPower;       // 공격력.
    [SerializeField] int towerLevel;          // 타워의 레벨.
    [SerializeField] int towerPrice;          // 타워의 가격.

    public TOWER_TYPE Type => type;
    public int TowerPrice => towerPrice;
    public int SellPrice => (int)(towerPrice * 0.6f);
    public int TowerLevel => towerLevel;
    public Sprite TowerSprite => towerSprite;
    

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

        //DrawRange(0.1f, attackRange);
    }
    private void Search()
    {
        // 1.적이 없거나 공격 범위에서 벗어나면 새로운 적을 탐색한다.
        // 2.탐색 우선 순위는 가장 가까운 거리의 적이다.
        if(enemy == null || Vector2.Distance(transform.position, enemy.transform.position) > attackRange)
        {
            enemy = null;

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

    public void DrawRange(float lineWidth, float radius)
    {
        var segments = 360;
        LineRenderer line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        line.SetPositions(points);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
