using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class UIControllerLobby : MonoBehaviour
{
    // ������Ʈ Ŭ����
    public UIDocument UI_Background;
    public UIDocument UI_Character;
    public UIDocument UI_LobbyUI;
    public UIDocument UI_Gacha;

    // �� UI�� Root VisualElement
    private VisualElement root_Background;
    private VisualElement root_Character;
    private VisualElement root_LobbyUI;
    private VisualElement root_Gacha;

    // Buttons
    private Button[] tabBtns;
    private Button toLobbyBtn;
    private Button buildBtn;
    private Button gachaUiMoreBtn;
    private Button gachaUiOkBtn;

    // ���� ���
    private List<VisualElement>[] tabElements;
    private VisualElement[] gachaPanels;
    private VisualElement lobbyBackImg;

    // ����
    private int currentTabNum;
    private int newTabNum;

    void Start()
    {
        SetRootElement();

        SetTabSettings(); // �� �ǹ�ư Ŭ�� �� ����(�����̵�) �� ��� ��� �߰�

        SetGachaUI(); // ��í UI ���� ����

        foreach (var tabBtn in tabBtns)
        {
            tabBtn.RegisterCallback<ClickEvent>(OnBottomTabBtnClicked);
        }

        buildBtn = root_LobbyUI.Q<Button>("Button-Build");
        buildBtn.RegisterCallback<ClickEvent>(OnBuildBtnClicked);

        gachaUiMoreBtn = root_Gacha.Q<Button>("Button-More");
        gachaUiMoreBtn.RegisterCallback<ClickEvent>(OnGachaUiReBuildBtnClicked);

        gachaUiOkBtn = root_Gacha.Q<Button>("Button-OK");
        gachaUiOkBtn.RegisterCallback<ClickEvent>(OnGachaUiOkBtnClicked);

        SetupYoyo();
    }

    private void SetRootElement()
    {
        root_Background = UI_Background.GetComponent<UIDocument>().rootVisualElement;
        root_Character = UI_Character.GetComponent<UIDocument>().rootVisualElement;
        root_LobbyUI = UI_LobbyUI.GetComponent<UIDocument>().rootVisualElement;
        root_Gacha = UI_Gacha.GetComponent<UIDocument>().rootVisualElement;
    }
    private void SetTabSettings()
    {
        currentTabNum = 4;

        tabElements = new List<VisualElement>[]{
            new List<VisualElement>(),
            new List<VisualElement>(),
            new List<VisualElement>(),
            new List<VisualElement>(),
            new List<VisualElement>(),
            new List<VisualElement>(),
            new List<VisualElement>(),
            new List<VisualElement>(),
            new List<VisualElement>(),
        };
        tabElements[0].Add(root_LobbyUI.Q<VisualElement>("Tab0_Shop"));
        tabElements[1].Add(root_LobbyUI.Q<VisualElement>("Tab1_Leniz"));
        tabElements[2].Add(root_LobbyUI.Q<VisualElement>("Tab2_Item"));
        tabElements[3].Add(root_LobbyUI.Q<VisualElement>("Tab3_Lab"));
        tabElements[4].Add(root_LobbyUI.Q<VisualElement>("Tab4_Lobby"));
        tabElements[5].Add(root_LobbyUI.Q<VisualElement>("Tab5_Gacha"));
        tabElements[6].Add(root_LobbyUI.Q<VisualElement>("Tab6_House"));
        tabElements[7].Add(root_LobbyUI.Q<VisualElement>("Tab7_Guild"));
        tabElements[8].Add(root_LobbyUI.Q<VisualElement>("Tab8_Square"));

        tabElements[4].Add(root_Character.Q<VisualElement>("Tab4"));
        tabElements[5].Add(root_Character.Q<VisualElement>("Tab5"));

        tabBtns = new Button[]{
            root_LobbyUI.Q<Button>("Btn_Shop"),
            root_LobbyUI.Q<Button>("Btn_Leniz"),
            root_LobbyUI.Q<Button>("Btn_Item"),
            root_LobbyUI.Q<Button>("Btn_Lab"),
            root_LobbyUI.Q<Button>("Btn_Lobby"),
            root_LobbyUI.Q<Button>("Btn_Gacha"),
            root_LobbyUI.Q<Button>("Btn_House"),
            root_LobbyUI.Q<Button>("Btn_Guild"),
            root_LobbyUI.Q<Button>("Btn_Square"),
        };
        lobbyBackImg = root_Background.Q<VisualElement>("BackgroundImage");
    }
    private void SetGachaUI()
    {
        gachaPanels = new VisualElement[]{
            root_Gacha.Q<VisualElement>("Panel0"),
            root_Gacha.Q<VisualElement>("Panel1"),
            root_Gacha.Q<VisualElement>("Panel2"),
            root_Gacha.Q<VisualElement>("Panel3"),
            root_Gacha.Q<VisualElement>("Panel4"),
            root_Gacha.Q<VisualElement>("Panel5"),
            root_Gacha.Q<VisualElement>("Panel6"),
            root_Gacha.Q<VisualElement>("Panel7"),
            root_Gacha.Q<VisualElement>("Panel8"),
            root_Gacha.Q<VisualElement>("Panel9"),
        };
    }
    private void SetupYoyo()
    {
        List<VisualElement> yoyoElements = new List<VisualElement>();
        yoyoElements.Add(root_Character.Q<VisualElement>("Tab4_Image"));
        yoyoElements.Add(root_Character.Q<VisualElement>("Tab5_Image"));

        foreach(var element in yoyoElements)
        {
            element.RegisterCallback<TransitionEndEvent>(evt => { element.ToggleInClassList("any--yoyo2p"); });
            element.schedule.Execute(_ => element.ToggleInClassList("any--yoyo2p")).StartingIn(100);
        }
    }

    private void OnBottomTabBtnClicked(ClickEvent evt)
    {
        newTabNum = Array.IndexOf(tabBtns, evt.currentTarget);
        //foreach (var newElement in tabElements[newTabNum])
        //    newElement.style.display = DisplayStyle.Flex;

        foreach (var oldElement in tabElements[currentTabNum])
        {
            EventCallback<TransitionEndEvent> onTransitionEnd = null;
            onTransitionEnd = (TransitionEndEvent e) =>
            {
                //oldElement.style.display = DisplayStyle.None;
                oldElement.UnregisterCallback(onTransitionEnd);
            };

            oldElement.RegisterCallback(onTransitionEnd);
        }

        for (int i = 0; i < tabElements.Length; i++)
        {
            foreach (var element in tabElements[i])
            {
                if (i < newTabNum)
                {
                    element.AddToClassList("tab--left");
                    element.RemoveFromClassList("tab--right");
                }
                else if (i == newTabNum)
                {
                    element.RemoveFromClassList("tab--left");
                    element.RemoveFromClassList("tab--right");
                }
                else
                {
                    element.RemoveFromClassList("tab--left");
                    element.AddToClassList("tab--right");
                }
            }
        }
        currentTabNum = newTabNum;
        lobbyBackImg.style.translate = new Translate(new Length(0f+(newTabNum-4f)*2f, LengthUnit.Percent), 0f);
    }
    private void OnBuildBtnClicked(ClickEvent evt)
    {
        TmpNewGachaTen();

        root_Gacha.Q<VisualElement>("Master").style.display = DisplayStyle.Flex;

    }
    private void TmpNewGachaTen()
    {
        for (int i = 0; i < 10; i++)
        {
            int randomIndex = Random.Range(1, 49); // 1���� 48���� ����
            string fileName = randomIndex.ToString("D4"); // 4�ڸ� ���ڷ� ������

            Texture2D texture = Resources.Load<Texture2D>($"Images/Character/Full/{fileName}");

            if (texture != null) gachaPanels[i].Q<VisualElement>("Pic").style.backgroundImage = new StyleBackground(texture);
            else Debug.LogWarning($"�ؽ��ĸ� ã�� �� ����: {fileName}");
        }
    }
    private void OnGachaUiReBuildBtnClicked(ClickEvent evt)
    {
        TmpNewGachaTen();
    }
    private void OnGachaUiOkBtnClicked(ClickEvent evt)
    {
        root_Gacha.Q<VisualElement>("Master").style.display = DisplayStyle.None;
    }
}