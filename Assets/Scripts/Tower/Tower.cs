using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
    public enum TOWER_TYPE
    {
        None = -1,
        Projectile,        // �Ϲ�.
        Expolde,           // ����.
    }

    [SerializeField] TOWER_TYPE type;
    
    [SerializeField] Sprite towerSprite;      // Ÿ���� �̹���.
    [SerializeField] Bullet bulletPrefab;     // �Ѿ�.
    [SerializeField] int towerLevel;          // Ÿ���� ����.
    [SerializeField] int towerPrice;          // Ÿ���� ����.

    [Header("Attack")]
    [SerializeField] STATE_TYPE attackType;   // ���� Ÿ��.
    [SerializeField] float attackRange;       // ���� ����.
    [SerializeField] float attackRate;        // ���� �ӵ�.
    [SerializeField] float attackPower;       // ���ݷ�.

    public TOWER_TYPE Type => type;
    public int TowerPrice => towerPrice;
    public int SellPrice => (int)(towerPrice * 0.6f);
    public int TowerLevel => towerLevel;
    public Sprite TowerSprite => towerSprite;

    LineRenderer line;
    float nextAttackTime = 0.0f;
    Enemy enemy;

    bool isSetTower = false;        // Ÿ���� ��ġ�� �����ΰ�?

    // ������Ʈ�� Ȱ��ȭ�Ǵ� ������ ȣ��.
    private void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        SetupRangeLine();                       // ���� �������� ��ġ ����.
    }
    private void Update()
    {
        // ���� ������ ���� ���� ���¿����� �۵����� �ʴ´�.
        if (isSetTower == false)
            return;

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

                // �˻��� �ݸ����� ���� �ƴϰų� Ȥ�� ������ �� ���� Ÿ���� ���.
                if (target == null || IsAttack(target) == false)
                    continue;

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
    private bool IsAttack(Enemy enemy)
    {
        // ��Ʈ �������� ���� �̵� Ÿ�԰� ���� ���� Ÿ���� ��.
        return (attackType & enemy.MoveType) != 0;
    }
    private void LookTarget()
    {
        Vector3 dir = enemy.transform.position - transform.position;
        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion lookAt = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.rotation = lookAt;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, 30f * Time.deltaTime);
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



    private void SetupRangeLine()
    {
        int segments = 360;
        line.useWorldSpace = false;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.positionCount = segments + 1;

        int pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        Vector3[] points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            float rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * attackRange, Mathf.Cos(rad) * attackRange, 0);
        }

        line.SetPositions(points);
        line.enabled = false;               // LineRenderer ��Ȱ��ȭ.
    }



    // �ܺ� �Լ�
    public void OnSetupTower()
    {
        isSetTower = true;
    }
    public void SwitchRange(bool isDraw)
    {
        line.enabled = isDraw;
    }
}
