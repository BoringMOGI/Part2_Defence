using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int life;
    [SerializeField] int gold;
    [SerializeField] UnityEvent OnGameOver;

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
        if(gold >= amount)              // 소지 골드가 요구량 이상인가?
        {
            gold -= amount;             // 요구량만큼 골드 소비.
            topBar.SetGoldText(gold);   // UI 업데이트.
            return true;                // true 리턴.
        }

        return false;
    }

    private void GameOver()
    {
        isGameOver = true;
        OnGameOver?.Invoke();       // 게임오버 이벤트 호출.
    }
}
