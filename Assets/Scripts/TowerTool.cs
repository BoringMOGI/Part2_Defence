using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerTool : Singleton<TowerTool>
{
    public enum TOOL_TYPE
    {
        Close,          // �������� ����.
        Upgrade,        // ���׷��̵�.
        Sell,           // �Ǹ�.
    }

    [SerializeField] Text upgradeText;
    [SerializeField] Text sellText;


    public delegate void OnSelectedToolEvent(TOOL_TYPE type);
    OnSelectedToolEvent OnSelectedTool;

    Tower selectedTarget;

    private void Start()
    {
        CloseTool();
    }

    public void OpenTool(Tower target, OnSelectedToolEvent OnSelectedTool)
    {
        gameObject.SetActive(true);

        this.OnSelectedTool = OnSelectedTool;
        selectedTarget = target;

        Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position = pos;

        // �ؽ�Ʈ ����.
        int upgradePrice = TowerSpawner.Instance.TowerPrice(target.Type, target.TowerLevel + 1);
        int sellPrice = target.SellPrice;

        upgradeText.text = upgradePrice.ToString("#,##0G");
        sellText.text = sellPrice.ToString("#,##0G");
    }
    public void CloseTool()
    {
        gameObject.SetActive(false);
    }

    public void OnSelectUpgrade()
    {
        OnSelectedTool?.Invoke(TOOL_TYPE.Upgrade);
        CloseTool();
    }
    public void OnSelectSell()
    {
        OnSelectedTool?.Invoke(TOOL_TYPE.Sell);
        CloseTool();
    }

}
