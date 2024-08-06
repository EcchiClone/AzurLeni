using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LogText : MonoBehaviour
{
    private static ScrollView logScrollView;

    // Awake is called before the first frame update
    void Awake()
    {
        logScrollView = GetComponent<UIDocument>().rootVisualElement.Q<ScrollView>("LogScrollView");
    }

    private static void ScrollToBottom(VisualElement item)
    {
        // ��ũ�� �̽� �ذ� https://discussions.unity.com/t/solved-scrollview-scroll-to-bottom/882578/7

        // ��� ���� ����� �ﰢ���� �α� ���� �ʿ���. ���پ� �и�. �Ʒ� �� ���� ����.
        // �� �ð� ���⿡ ������ �����ؼ� �ϴ� �ذ� ����
        //logScrollView.verticalScroller.value = logScrollView.verticalScroller.highValue > 0 ? logScrollView.verticalScroller.highValue : 0;
        //StartCoroutine(ScrollToItemNextFrame(item));
        logScrollView.schedule.Execute(() => logScrollView.ScrollTo(item)); // ��� �� ������ �ִ� ��� ����
    }

    public static void AddLog(string message, LogSign _logSign = LogSign.Normal)
    {
        try
        {
            // ���ο� Label ����
            Label newLogLabel = new Label();
            newLogLabel.text = $"[System] {message}";
            newLogLabel.AddToClassList("log-label");
            if (_logSign == LogSign.Warning)
            {
                newLogLabel.AddToClassList("log-warning");
            }
            if (_logSign == LogSign.Error)
            {
                newLogLabel.AddToClassList("log-error");
            }


            // ScrollView�� Label �߰�
            logScrollView.Add(newLogLabel);

            // ��ũ���� ���� �Ʒ��� �̵�
            ScrollToBottom(newLogLabel);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public static void ClearLog()
    {
        try
        {
            logScrollView.Clear();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private IEnumerator ScrollToItemNextFrame(VisualElement item)
    {
        yield return null; // �� ������ ���

        // logScrollView�� ScrollTo �޼��带 1������ �ڿ� ȣ��
        logScrollView.ScrollTo(item);
    }
}
