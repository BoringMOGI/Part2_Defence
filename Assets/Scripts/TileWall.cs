using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWall : MonoBehaviour
{
    private Tower onTower;                          // ���� �ִ� Ÿ��.
    public bool IsOnTower => onTower != null;       // Ÿ���� ��ġ�Ǿ� �ִ°�?

    static System.Action OnDisableAllFrame;         // ��� Ÿ���� �������� ���� �̺�Ʈ.

    [SerializeField] GameObject selectFrame;


    private void Start()
    {
        // ���ٽ��� �̿��� ��������Ʈ�� �Լ� ���.
        OnDisableAllFrame += () => { SwitchFrame(false); };
        SwitchFrame(false);
    }

    bool isSelected;

    private void OnMouseEnter()             // ���콺 �����Ͱ� ������ ��.
    {
        OnDisableAllFrame?.Invoke();
        SwitchFrame(true);
    }
    private void OnMouseExit()              // ���콺 �����Ͱ� ������ ��.
    {
        SwitchFrame(false);
    }
    private void OnMouseUpAsButton()        // ���콺�� Ŭ������ ��.
    {
        
    }

    private void SwitchFrame(bool isShow)
    {
        selectFrame.SetActive(isShow);
        isSelected = isShow;
    }

    public void OnSelectedTile()
    {
        if(IsOnTower)
        {
            OnDisableAllFrame?.Invoke();                                    // ������ Ÿ�� ������ ����.
            SwitchFrame(true);                                              // ���� Ÿ�� ������ �ѱ�.

            TowerTool.Instance.OpenTool(transform);                         // Tool UI ���.
        }
        else
        {
            Tower newTower = TowerSpawner.Instance.GetCurrentTower();       // Ÿ�� �����ʿ��� ���ο� Ÿ���� �޾ƿ´�.
            newTower.transform.position = transform.position;               // �ش� Ÿ���� �������� ������ �����.
            onTower = newTower;                                             // ���� ������ �ִ� Ÿ���� ����.
        }
    }

    
}
