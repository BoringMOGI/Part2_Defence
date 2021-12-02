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
    }
    private void Update()
    {
        topBar.SetGoldText(gold);
        topBar.SetLifeText(life);
    }

    public void OnDamagedLife(int amount)
    {
        if (isGameOver)
            return;

        life = Mathf.Clamp(life - amount, 0, 999);      // life�� �ּ� �ִ� �� ����.
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
        if(gold >= amount)              // ���� ��尡 �䱸�� �̻��ΰ�?
        {
            gold -= amount;             // �䱸����ŭ ��� �Һ�.
            return true;                // true ����.
        }

        return false;
    }

    private void GameOver()
    {
        isGameOver = true;
        OnGameOver?.Invoke();       // ���ӿ��� �̺�Ʈ ȣ��.

        Invoke("OpenNotice", 1.0f);
    }

    private void OpenNotice()
    {
        // ���ٽ� : �̸� ���� �ӽ� �Լ�.
        Notice.Instance.OpenNotice("����� �ϰڽ��ϱ�?", "��", "�ƴϿ�", (Notice.RESPONSE response) => {

            if (response == Notice.RESPONSE.Yes)        // ����.
            {
                SceneManager.Instance.LoadNextScene("Game");
            }
            else if (response == Notice.RESPONSE.No)   // ����.
            {
                SceneManager.Instance.LoadNextScene("Main");
            }
        });
    }
    
}
