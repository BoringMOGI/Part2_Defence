using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] int getGold;
    [SerializeField] float moveSpeed;

    Transform [] vectors;           // 내가 가야할 목적지.
    int vectorIndex;                // 목적지 번호.

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
        GameManager.Instance.OnDamagedLife(1);      // GameManager의 싱글톤에 접근해 라이프에 데미지를 준다.
        Destroy(gameObject);
    }
    private void MoveTo()
    {
        Vector3 destination = vectors[vectorIndex].position;        // 목적지.
        Vector3 currentPos = transform.position;                    // 현재 위치.

        // 목적지 위치 - 내 위치 = 목적지 방향.
        // Vector3 direction = destination - currentPos;
        // direction.Normalize();

        // MoveTowards(내위치, 목적지, 속도)
        transform.position = Vector3.MoveTowards(currentPos, destination, moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Vector3 destination = vectors[vectorIndex].position;        // 목적지.
        Vector3 currentPos = transform.position;                    // 현재 위치.

        if (destination == currentPos)                              // 목적지에 도착했을 경우.
        {
            vectorIndex++;
            if (vectors.Length <= vectorIndex)                      // 마지막 위치에 도달했다.
            {
                OnGoal();
            }
        }
        else
        {
            MoveTo();                   // 이동.
        }
    }
}
