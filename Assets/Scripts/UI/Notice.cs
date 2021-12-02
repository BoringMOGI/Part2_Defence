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
        // �ŰԺ����� �̿��� Text ����.
        panel.SetActive(true);
        contentText.text = content;
        yesText.text = yes;
        noText.text = no;

        // ������ ������ �ݹ� �Լ� ����.
        this.callback = callback;
    }
    public void OnSelectedButton(int index)
    {
        // ����.
        callback?.Invoke((RESPONSE)index);
        panel.SetActive(false);
    }
}
