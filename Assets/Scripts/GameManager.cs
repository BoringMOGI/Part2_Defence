using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int life;
    [SerializeField] int gold;

    TopBarUI topBar;            // 상단바 UI.

    // 자동 프로퍼티.
    public bool isGameOver
    {
        get;                    // 값을 참조하는 것은 public.
        private set;            // 값을 대입하는 것은 private.
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

        life = Mathf.Clamp(life - amount, 0, 999);      // life의 최소 최대 값 제한.
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
