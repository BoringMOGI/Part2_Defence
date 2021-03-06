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
        AudioManager.Instance.PlayBgm("MainBGM");
    }
    private void Update()
    {
        topBar.SetGoldText(gold);
        topBar.SetLifeText(life);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayEffect("magic_03");
        }
    }

    public void OnDamagedLife(int amount)
    {
        if (isGameOver)
            return;

        life = Mathf.Clamp(life - amount, 0, 999);      // life의 최소 최대 값 제한.
        if(life <= 0)
        {
            GameOver();
        }
    }
    public void OnGetGold(int amount)
    {
        gold += amount;
    }

    public bool UseGold(int amount)
    {
        if(gold >= amount)              // 소지 골드가 요구량 이상인가?
        {
            gold -= amount;             // 요구량만큼 골드 소비.
            return true;                // true 리턴.
        }

        return false;
    }

    private void GameOver()
    {
        isGameOver = true;
        OnGameOver?.Invoke();       // 게임오버 이벤트 호출.

        Invoke("OpenNotice", 1.0f);
    }

    private void OpenNotice()
    {
        // 람다식 : 이름 없는 임시 함수.
        Notice.Instance.OpenNotice("재시작 하겠습니까?", "네", "아니오", (Notice.RESPONSE response) => {

            if (response == Notice.RESPONSE.Yes)        // 긍정.
            {
                SceneManager.Instance.LoadNextScene(SceneManager.SCENE.Game);
            }
            else if (response == Notice.RESPONSE.No)   // 부정.
            {
                SceneManager.Instance.LoadNextScene(SceneManager.SCENE.Main);
            }
        });
    }
    
}
