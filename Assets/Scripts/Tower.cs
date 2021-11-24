using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform bulletPrefab;  // �Ѿ�.
    [SerializeField] float attackRange;       // ���� ����.
    [SerializeField] float attackRate;        // ���� �ӵ�.
    [SerializeField] float attackPower;       // ���ݷ�.

    float nextAttackTime = 0.0f;

    private void Update()
    {
        if(nextAttackTime <= Time.time)                     // ���� �ð��� �Ǿ��ٸ�.
        {
            nextAttackTime = Time.time + attackRate;        // ���� �ð� ����.
            Enemy enemy = Target();                         // ���� Ž��.
            if (enemy != null)                              // ���� Ž���Ǿ��ٸ�.
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
                float distance = Vector2.Distance(transform.position, target.transform.position);   // ���� ����� �Ÿ�.
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
