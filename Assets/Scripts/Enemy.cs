using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] int getGold;
    [SerializeField] float moveSpeed;

    Transform [] vectors;           // ���� ������ ������.
    int vectorIndex;                // ������ ��ȣ.

    public void Setup(Transform[] vectors)
    {
        this.vectors = vectors;
    }
    public void OnDamaged(float power)
    {
        hp -= power;
        if(hp <= 0)
        {
            GameManager.Instance.OnGetGold(getGold);
            Destroy(gameObject);
        }
    }

    private void OnGoal()
    {
        GameManager.Instance.OnDamagedLife(1);      // GameManager�� �̱��濡 ������ �������� �������� �ش�.
        Destroy(gameObject);
    }
    private void MoveTo()
    {
        Vector3 destination = vectors[vectorIndex].position;        // ������.
        Vector3 currentPos = transform.position;                    // ���� ��ġ.

        // ������ ��ġ - �� ��ġ = ������ ����.
        // Vector3 direction = destination - currentPos;
        // direction.Normalize();

        // MoveTowards(����ġ, ������, �ӵ�)
        transform.position = Vector3.MoveTowards(currentPos, destination, moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Vector3 destination = vectors[vectorIndex].position;        // ������.
        Vector3 currentPos = transform.position;                    // ���� ��ġ.

        if (destination == currentPos)                              // �������� �������� ���.
        {
            vectorIndex++;
            if (vectors.Length <= vectorIndex)                      // ������ ��ġ�� �����ߴ�.
            {
                OnGoal();
            }
        }
        else
        {
            MoveTo();                   // �̵�.
        }
    }
}
