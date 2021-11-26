using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int life;
    [SerializeField] int gold;
    [SerializeField] UnityEvent OnGameOver;

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
            GameOver();
        }
    }
    public void OnGetGold(int amount)
    {
        gold += amount;
        topBar.SetGoldText(gold);
    }

    public bool UseGold(int amount)
    {
        if(gold >= amount)              // ���� ��尡 �䱸�� �̻��ΰ�?
        {
            gold -= amount;             // �䱸����ŭ ��� �Һ�.
            topBar.SetGoldText(gold);   // UI ������Ʈ.
            return true;                // true ����.
        }

        return false;
    }

    private void GameOver()
    {
        isGameOver = true;
        OnGameOver?.Invoke();       // ���ӿ��� �̺�Ʈ ȣ��.
    }
}
