using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : Tower
{
    [SerializeField] int level;
    [SerializeField] int maxLevel;

    public void Upgrade()
    {
        if (level >= maxLevel)
            return;

    }


}
