using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TOWER_TYPE
    {
        Projectile,        // �Ϲ�.
        Expolde,           // ����.
    }

    [SerializeField] TOWER_TYPE type;
    [SerializeField] Sprite towerSprite;      // Ÿ���� �̹���.
    [SerializeField] Bullet bulletPrefab;     // �Ѿ�.
    [SerializeField] float attackRange;       // ���� ����.
    [SerializeField] float attackRate;        // ���� �ӵ�.
    [SerializeField] float attackPower;       // ���ݷ�.
    [SerializeField] int towerLevel;          // Ÿ���� ����.
    [SerializeField] int towerPrice;          // Ÿ���� ����.

    public TOWER_TYPE Type => type;
    public int TowerPrice => towerPrice;
    public int SellPrice => (int)(towerPrice * 0.6f);
    public int TowerLevel => towerLevel;
    public Sprite TowerSprite => towerSprite;
    

    float nextAttackTime = 0.0f;
    Enemy enemy;

    private void Update()
    {
        Search();                             // ���� Ž��.

        if (enemy != null)
        {
            LookTarget();
            Fire();
        }

        //DrawRange(0.1f, attackRange);
    }
    private void Search()
    {
        // 1.���� ���ų� ���� �������� ����� ���ο� ���� Ž���Ѵ�.
        // 2.Ž�� �켱 ������ ���� ����� �Ÿ��� ���̴�.
        if(enemy == null || Vector2.Distance(transform.position, enemy.transform.position) > attackRange)
        {
            enemy = null;

            // ���ο� �� Ž��.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
            float minDistance = attackRange;

            // ��� �ݸ����� ��ȸ.
            foreach (Collider2D collider in colliders)
            {
                Enemy target = collider.GetComponent<Enemy>();
                if (target != null)
                {
                    // ������ �Ÿ� ���� �� ���� �ּ� �Ÿ��� ���� �����Ѵ�.
                    float distance = Vector2.Distance(transform.position, target.transform.position);   // ���� ����� �Ÿ�.
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
        if (nextAttackTime <= Time.time)                    // ���� �ð��� �Ǿ��ٸ�.
        {
            nextAttackTime = Time.time + attackRate;        // ���� �ð� ����.            
            if (enemy != null)                              // ���� Ž���Ǿ��ٸ�.
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
