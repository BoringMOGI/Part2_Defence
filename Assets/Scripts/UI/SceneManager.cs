using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] Image panel;

    Color fadeColor;
    bool isLoading;

    void Start()
    {
        fadeColor = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(FadeIn(1.0f));
    }

    public void LoadNextScene(string nextScene, float fadeTime = 1.0f)
    {
        if (isLoading)
            return;

        isLoading = true;
        StartCoroutine(FadeOut(fadeTime, nextScene));
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
