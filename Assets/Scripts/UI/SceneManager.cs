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
        panel.gameObject.SetActive(true);       // 패널을 활성화.
        panel.color = fadeColor;                // 색상은 검정 불투명.

        float time = fadeTime;

        while (true)
        {
            // time값을 시간값으로 빼는데 0.0f 이하가 되면 panel을 끄고 while문을 종료한다.
            if((time -= Time.deltaTime) <= 0.0f)
            {
                panel.gameObject.SetActive(false);
                break;
            }

            fadeColor.a = time / fadeTime;      // fade 색상의 알파값을 줄여간다.
            panel.color = fadeColor;            // 색상을 대입.

            yield return null;
        }

        fadeColor.a = 0f;                       // 알파값을 0으로 고정.
    }
    IEnumerator FadeOut(float fadeTime, string nextScene)
    {
        panel.gameObject.SetActive(true);                   // 패널 활성화.
        panel.color = fadeColor;                            // Fade 색상 대입.

        float time = 0.0f;

        while(true)
        {
            if ((time += Time.deltaTime) >= fadeTime)       // time에 시간 값을 더한다.
                break;

            fadeColor.a = time / fadeTime;                  // 알파 값 계산.
            panel.color = fadeColor;                        // panel에 색상 대입.

            yield return null;
        }

        fadeColor.a = 1f;                                   // 알파값을 1로 고정.
        panel.color = fadeColor;                            // panel에 색상 대입.

        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);  // 다음 씬을 로드.
    }
    
}
