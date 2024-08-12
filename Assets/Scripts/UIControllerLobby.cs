using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class UIControllerLobby : MonoBehaviour
{
    // 컴포넌트 클래스
    public UIDocument UI_Background;
    public UIDocument UI_Character;
    public UIDocument UI_LobbyUI;
    public UIDocument UI_Gacha;

    // 각 UI의 Root VisualElement
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

    // 동적 요소
    private List<VisualElement>[] tabElements;
    private VisualElement[] gachaPanels;
    private VisualElement lobbyBackImg;
    private VisualElement acquiredCharacterPanel;
    private VisualElement unacquiredCharacterPanel;

    // 변수
    private int currentTabNum;
    private int newTabNum;


    void Start()
    {
        SetRootElement();

        SetTabSettings(); // 각 탭버튼 클릭 시 반응(슬라이드) 할 모든 요소 추가

        SetGachaUI(); // 가챠 UI 관련 세팅

        SetEtcSettings();

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
    private void SetEtcSettings()
    {
        acquiredCharacterPanel = root_LobbyUI.Q<VisualElement>("AcquiredCharacterPanel");
        unacquiredCharacterPanel = root_LobbyUI.Q<VisualElement>("UnacquiredCharacterPanel");
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
        if(currentTabNum==1) { DockViewUpdate(); }
        lobbyBackImg.style.translate = new Translate(new Length(0f+(newTabNum-4f)*2f, LengthUnit.Percent), 0f);
    }

    // Tab5 가챠 관련
    private void OnBuildBtnClicked(ClickEvent evt)
    {
        TmpNewGachaTen();

        root_Gacha.Q<VisualElement>("Master").style.display = DisplayStyle.Flex;

    }
    private void TmpNewGachaTen()
    {
        for (int i = 0; i < 10; i++)
        {
            int randomIndex = Random.Range(1, DataManager._GameData.totalCharacterCount); // 1부터 totalCharacterCount-1까지 포함

            var characterBase = DataUtils.GetCharacterBaseWithID(randomIndex);

            var userCharacter = DataUtils.GetUserCharacterWithID(randomIndex);

            Texture2D texture = Resources.Load<Texture2D>($"Images/Character/{characterBase.imgFullPath}");

            userCharacter.exp += 1; // 테스트용
            if(userCharacter.level == 0) { userCharacter.level += 1; } // 테스트용

            if (texture != null) gachaPanels[i].Q<VisualElement>("Pic").style.backgroundImage = new StyleBackground(texture);
            else Debug.LogWarning($"텍스쳐를 찾을 수 없음: {characterBase.imgFullPath}");
        }
        NetworkManager.instance.UploadUserCharacterData();
    }
    private void OnGachaUiReBuildBtnClicked(ClickEvent evt)
    {
        TmpNewGachaTen();
    }
    private void OnGachaUiOkBtnClicked(ClickEvent evt)
    {
        root_Gacha.Q<VisualElement>("Master").style.display = DisplayStyle.None;
    }

    // Tab1 도크 관련
    private void DockViewUpdate()
    {
        acquiredCharacterPanel.Clear();
        unacquiredCharacterPanel.Clear();

        foreach (UserCharacter character in DataManager._UserData.character)
        {
            if(character.level > 0)
            {
                acquiredCharacterPanel.Add(CreateCharacterPanel(character.id));
            }
            else
            {
                unacquiredCharacterPanel.Add(CreateCharacterPanel(character.id));
            }
        } 
    }
    private VisualElement CreateCharacterPanel(int _characterNum)
    {
        VisualElement characterPanel = new VisualElement();
        VisualElement image = new VisualElement();
        VisualElement nameTag = new VisualElement();
        Label nameText = new Label();

        characterPanel.AddToClassList("characterPanel");
        image.AddToClassList("characterPanel-image");
        nameTag.AddToClassList("characterPanel-nameTag");
        nameText.AddToClassList("characterPanel-nameText");

        var character = DataUtils.GetCharacterBaseWithID(_characterNum);

        Texture2D texture = Resources.Load<Texture2D>($"Images/Character/{character.imgFullPath}");
        if (texture != null) image.style.backgroundImage = new StyleBackground(texture);

        nameText.text = character.name_en.Split()[0];

        characterPanel.Add(image);
        characterPanel.Add(nameTag);
        nameTag.Add(nameText);

        return characterPanel;
    }

}
