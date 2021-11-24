using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWall : MonoBehaviour
{
    private Tower onTower;                          // ���� �ִ� Ÿ��.
    public bool IsOnTower => onTower != null;       // Ÿ���� ��ġ�Ǿ� �ִ°�?

    public void SetTower(Tower tower)
    {
        tower.transform.position = transform.position;      // Ÿ���� ��ġ�� Ÿ���� ��ġ�� �����.
        onTower = tower;
    }
    
}
