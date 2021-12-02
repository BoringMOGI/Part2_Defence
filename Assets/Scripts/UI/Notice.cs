using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notice : Singleton<Notice>
{
    public enum RESPONSE
    {
        Yes,
        No,
    }

    [SerializeField] GameObject panel;
    [SerializeField] Text contentText;
    [SerializeField] Text yesText;
    [SerializeField] Text noText;

    public delegate void CallBackEvent(RESPONSE response);
    CallBackEvent callback;

    public void OpenNotice(string content, string yes, string no, CallBackEvent callback)
    {
        // 매게변수를 이용해 Text 세팅.
        panel.SetActive(true);
        contentText.text = content;
        yesText.text = yes;
        noText.text = no;

        // 응답을 전해줄 콜백 함수 대입.
        this.callback = callback;
    }
    public void OnSelectedButton(int index)
    {
        // 응답.
        callback?.Invoke((RESPONSE)index);
        panel.SetActive(false);
    }
}
