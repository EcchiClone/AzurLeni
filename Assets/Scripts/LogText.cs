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
        // 스크롤 이슈 해결 https://discussions.unity.com/t/solved-scrollview-scroll-to-bottom/882578/7

        // 통신 없는 경우의 즉각적인 로그 대응 필요함. 한줄씩 밀림. 아래 두 구문 실패.
        // 더 시간 쓰기에 낭비라고 생각해서 일단 해결 보류
        //logScrollView.verticalScroller.value = logScrollView.verticalScroller.highValue > 0 ? logScrollView.verticalScroller.highValue : 0;
        //StartCoroutine(ScrollToItemNextFrame(item));
        logScrollView.schedule.Execute(() => logScrollView.ScrollTo(item)); // 통신 등 스케쥴 있는 경우 대응
    }

    public static void AddLog(string message, LogSign _logSign = LogSign.Normal)
    {
        try
        {
            // 새로운 Label 생성
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


            // ScrollView에 Label 추가
            logScrollView.Add(newLogLabel);

            // 스크롤을 가장 아래로 이동
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
        yield return null; // 한 프레임 대기

        // logScrollView의 ScrollTo 메서드를 1프레임 뒤에 호출
        logScrollView.ScrollTo(item);
    }
}
