using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    public enum SCENE
    {
        Main,
        Game,
        Option,
    }

    [SerializeField] Image panel;

    private static SCENE currentScene;

    Color fadeColor;
    bool isLoading;

    void Start()
    {
        fadeColor = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(FadeIn(1.0f));
    }

    public void LoadNextScene(SCENE nextScene, float fadeTime = 1.0f)
    {
        if (isLoading)
            return;

        isLoading = true;
        currentScene = nextScene;
        StartCoroutine(FadeOut(fadeTime, nextScene.ToString()));
    }
    public void OpenOption()
    {
        // LoadSceneMode.Single   : �ش� �� �ϳ��� �ܵ����� �ҷ����ڴ�. (������ �ִ� ���� �� ��ε� �ȴ�.)
        // LoadSceneMode.Additive : �ش� ���� �߰��ؼ� �ε��Ѵ�. (������ �ִ� ���� ��ε���� �ʴ´�.)
        UnityEngine.SceneManagement.SceneManager.LoadScene(SCENE.Option.ToString(), LoadSceneMode.Additive);
    }
    public void CloseOption()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(SCENE.Option.ToString());
    }


    IEnumerator FadeIn(float fadeTime)
    {
        panel.gameObject.SetActive(true);       // �г��� Ȱ��ȭ.
        panel.color = fadeColor;                // ������ ���� ������.

        float time = fadeTime;

        while (true)
        {
            // time���� �ð������� ���µ� 0.0f ���ϰ� �Ǹ� panel�� ���� while���� �����Ѵ�.
            if((time -= Time.deltaTime) <= 0.0f)
            {
                panel.gameObject.SetActive(false);
                break;
            }

            fadeColor.a = time / fadeTime;      // fade ������ ���İ��� �ٿ�����.
            panel.color = fadeColor;            // ������ ����.

            yield return null;
        }

        fadeColor.a = 0f;                       // ���İ��� 0���� ����.
    }
    IEnumerator FadeOut(float fadeTime, string nextScene)
    {
        panel.gameObject.SetActive(true);                   // �г� Ȱ��ȭ.
        panel.color = fadeColor;                            // Fade ���� ����.

        float time = 0.0f;

        while(true)
        {
            if ((time += Time.deltaTime) >= fadeTime)       // time�� �ð� ���� ���Ѵ�.
                break;

            fadeColor.a = time / fadeTime;                  // ���� �� ���.
            panel.color = fadeColor;                        // panel�� ���� ����.

            yield return null;
        }

        fadeColor.a = 1f;                                   // ���İ��� 1�� ����.
        panel.color = fadeColor;                            // panel�� ���� ����.

        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);  // ���� ���� �ε�.
    }
    
}
