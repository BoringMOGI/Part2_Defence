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
    private void SwitchFrame(bool isShow)
    {
        selectFrame.SetActive(isShow);
        isSelected = isShow;
    }

    // Ÿ�� ���� ���콺 ������ ��ġ.
    private void OnMouseOver()
    {
        if (onTower == null)
            return;

        onTower.SwitchRange(true);
    }
    private void OnMouseExit()
    {
        if (onTower == null)
            return;

        onTower.SwitchRange(false);
    }
    // Ÿ���� �����ߴ�.
    public void OnSelectedTile()
    {
        if (IsOnTower)
        {
            OnDisableAllFrame?.Invoke();                                    // ������ Ÿ�� ������ ����.
            SwitchFrame(true);                                              // ���� Ÿ�� ������ �ѱ�.

            TowerTool.Instance.OpenTool(onTower, OnSelectedTowerTool);      // Tool UI ���.
        }
    }

    public void OnSetupTower(Tower newTower)
    {
        if (newTower == null)
            return;

        if (onTower)
            Destroy(onTower.gameObject);                                 // ������ ��ġ�� Ÿ���� ����.

        newTower.transform.position = transform.position;                // �ش� Ÿ���� �������� ������ �����.
        newTower.OnSetupTower();                                         // �ش� Ÿ���� ��ġ�Ǿ����� �˸�.
        onTower = newTower;                                              // ���� ������ �ִ� Ÿ���� ����.
    }
    private void OnSelectedTowerTool(TowerTool.TOOL_TYPE type)
    {
        switch(type)
        {
            case TowerTool.TOOL_TYPE.Close:
                break;

            case TowerTool.TOOL_TYPE.Upgrade:
                /*
                if(onTower.TowerLevel < 3)
                {
                    Tower nextLevelTower = TowerSpawner.Instance.GetTowerObject(onTower.Type, onTower.TowerLevel + 1);
                    OnSetupTower(nextLevelTower);
                }
                */
                break;

            case TowerTool.TOOL_TYPE.Sell:
                if(onTower != null)
                {
                    int getGold = onTower.SellPrice;                    // ��ġ�� Ÿ���� ����.
                    GameManager.Instance.OnGetGold(getGold);            // �ش� ���ݸ�ŭ GM���� ����.
                    Destroy(onTower.gameObject);                        // ��ġ�� Ÿ�� ����.
                }
                break;
        }

        OnDisableAllFrame?.Invoke();                                    // ������ Ÿ�� ������ ����.
    }
    
}
