using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTool : Singleton<TowerTool>
{
    private void Start()
    {
        CloseTool();
    }

    public void OpenTool(Transform pivot)
    {
        gameObject.SetActive(true);

        Vector3 pos = Camera.main.WorldToScreenPoint(pivot.position);
        transform.position = pos;
    }
    public void CloseTool()
    {
        gameObject.SetActive(false);
    }

    public void OnSelectUpgrade()
    {

    }
    public void OnSelectSell()
    {

    }

}
