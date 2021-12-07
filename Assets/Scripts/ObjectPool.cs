using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<PoolType> : MonoBehaviour
    where PoolType : MonoBehaviour, ObjectPool<PoolType>.IPool
{
    public interface IPool
    {
        void Setup(OnReturnPoolEvent OnReturnPool);
    }

    public delegate void OnReturnPoolEvent(PoolType type);

    [SerializeField] Transform storageParent;       // ����� �θ� ������Ʈ.
    [SerializeField] PoolType prefab;               // Ǯ�� ������Ʈ ������.
    [SerializeField] int createCount;               // ���� ���� ����.

    Stack<PoolType> storage = new Stack<PoolType>();

    protected void Awake()
    {
        storageParent.gameObject.SetActive(false);  // ����� ������Ʈ ��Ȱ��ȭ.
        CreatePool(createCount);
    }

    private void CreatePool(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            PoolType newPool = Instantiate(prefab, storageParent);
            newPool.Setup(OnReturnPool);
            storage.Push(newPool);
        }
    }


    public PoolType GetPool()
    {
        if (storage.Count <= 0)
            CreatePool();

        PoolType pop = storage.Pop();               // ����ҿ��� �ϳ� ������.
        pop.transform.SetParent(transform);         // ������Ʈ�� �θ� ����.

        return pop;                                 // ������Ʈ ��ȯ.
    }
    public void OnReturnPool(PoolType pool)
    {
        pool.transform.SetParent(storageParent);    // ���ƿ� Ǯ�� ������Ʈ�� �θ� ����.
        storage.Push(pool);                         // stack ��⿭�� �߰�.
    }
}
