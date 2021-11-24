using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Transform [] vectors;           // ���� ������ ������.
    int vectorIndex;                // ������ ��ȣ.

    public void Setup(Transform[] vectors)
    {
        this.vectors = vectors;
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
                Destroy(gameObject);
            }
        }
        else
        {
            // ������ ��ġ - �� ��ġ = ������ ����.
            // Vector3 direction = destination - currentPos;
            // direction.Normalize();

            // MoveTowards(����ġ, ������, �ӵ�)
            transform.position = Vector3.MoveTowards(currentPos, destination, moveSpeed * Time.deltaTime);
        }
    }
}
