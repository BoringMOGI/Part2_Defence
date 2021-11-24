using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWall : MonoBehaviour
{
    private Tower onTower;                          // 위에 있는 타워.
    public bool IsOnTower => onTower != null;       // 타워가 설치되어 있는가?

    public void SetTower(Tower tower)
    {
        tower.transform.position = transform.position;      // 타워의 위치를 타일의 위치에 맞춘다.
        onTower = tower;
    }
    
}
