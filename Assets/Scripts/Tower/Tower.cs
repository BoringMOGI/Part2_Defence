using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TOWER_TYPE
    {
        Projectile,        // �Ϲ�.
        Projectile2,
        Projectile3,
        Expolde,           // ����.
        Expolde2,
        Expolde3,
    }

    [SerializeField] TOWER_TYPE type;
    [SerializeField] Bullet bulletPrefab;     // �Ѿ�.
    [SerializeField] float attackRange;       // ���� ����.
    [SerializeField] float attackRate;        // ���� �ӵ�.
    [SerializeField] float attackPower;       // ���ݷ�.
    [SerializeField] int towerPrice;          // Ÿ���� ����.

    public int TowerPrice => towerPrice;

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
    }
    private void Search()
    {
        // 1.���� ���ų� ���� �������� ����� ���ο� ���� Ž���Ѵ�.
        // 2.Ž�� �켱 ������ ���� ����� �Ÿ��� ���̴�.
        if(enemy == null || Vector2.Distance(transform.position, enemy.transform.position) > attackRange)
        {
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
