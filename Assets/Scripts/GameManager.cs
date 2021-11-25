using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int life;
    [SerializeField] int gold;

    TopBarUI topBar;            // ��ܹ� UI.

    // �ڵ� ������Ƽ.
    public bool isGameOver
    {
        get;                    // ���� �����ϴ� ���� public.
        private set;            // ���� �����ϴ� ���� private.
    }

    private void Start()
    {
        topBar = TopBarUI.Instance;
        topBar.SetGoldText(gold);
        topBar.SetLifeText(life);
    }

    public void OnDamagedLife(int amount)
    {
        if (isGameOver)
            return;

        life = Mathf.Clamp(life - amount, 0, 999);      // life�� �ּ� �ִ� �� ����.
        topBar.SetLifeText(life);
        if(life <= 0)
        {
            OnGameOver();
        }
    }
    public void OnGetGold(int amount)
    {
        gold += amount;
        topBar.SetGoldText(gold);
    }

    private void OnGameOver()
    {
        isGameOver = true;
        Debug.Log("GameOver");
    }
}
