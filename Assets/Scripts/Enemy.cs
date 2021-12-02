using UnityEngine;

public class Enemy : Unit, IHealth
{
    public enum TYPE
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
    }

    [SerializeField] STATE_TYPE moveType;
    [SerializeField] Transform pivot;
    [SerializeField] Transform hpBarPivot;
    [SerializeField] float maxHp;
    [SerializeField] int getGold;
    [SerializeField] float moveSpeed;

    public STATE_TYPE MoveType => moveType;
    public Vector3 DamagePivot => pivot.position;


    System.Action OnDeadEvent;      // 죽었을때 발생하는 이벤트.
    Transform[] vectors;            // 내가 가야할 목적지.
    int vectorIndex;                // 목적지 번호.
    float hp;                       // 현재 체력.
    bool isAlive;                   // 생존 여부.

    public void Setup(Transform[] vectors, System.Action OnDeadEvent)
    {
        this.vectors = vectors;                 // 나아가야할 벡터 대입.
        this.OnDeadEvent = OnDeadEvent;         // 죽었을때 이벤트.
        hp = maxHp;                             // 현재 체력을 최대 체력으로 변환.
        isAlive = true;

        HpBarUI.Instance.SetHpBar(this);        // 체력바UI에게 나의 인터페이스를 전달.
    }
    public void OnDamaged(float power)
    {
        hp -= power;
        if (hp <= 0)
        {
            GameManager.Instance.OnGetGold(getGold);
            OnDead();
        }
    }

    private void OnGoal()
    {
        GameManager.Instance.OnDamagedLife(1);      // GameManager의 싱글톤에 접근해 라이프에 데미지를 준다.
        OnDead();
    }
    private void OnDead()
    {
        OnDeadEvent?.Invoke();          // 해당 이벤트의 참조가 null이지 않을 경우 호출.
        isAlive = false;
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

    public float GetCurrentHP()
    {
        return hp;
    }

    public float GetMaxHp()
    {
        return maxHp;
    }

    public Vector3 GetPosition()
    {
        if (hpBarPivot == null)
            return Vector3.zero;
        else
            return hpBarPivot.position;
    }
    public bool IsAlive()
    {
        return isAlive;
    }
}
