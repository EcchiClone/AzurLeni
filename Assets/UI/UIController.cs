using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using System.Linq;
using UnityEditorInternal;
using static UnityEngine.GraphicsBuffer;

public class UIController : MonoBehaviour
{
    public UIDocument UI_Background;
    public UIDocument UI_Character;
    public UIDocument UI_LobbyUI;
    public UIDocument UI_Gacha;

    private VisualElement root_Background;
    private VisualElement root_Character;
    private VisualElement root_LobbyUI;
    private VisualElement root_Gacha;

    private VisualElement _lobbyBackImg;
    private VisualElement[] _tabs;
    private VisualElement[] _subTabs;
    private Button[] _tabBtns;
    private Button _toLobby;
    private int _crtTab;
    private int _newTab;

    void Start()
    {
        root_Background = UI_Background.GetComponent<UIDocument>().rootVisualElement;
        root_Character = UI_Character.GetComponent<UIDocument>().rootVisualElement;
        root_LobbyUI = UI_LobbyUI.GetComponent<UIDocument>().rootVisualElement;
        root_Gacha = UI_Gacha.GetComponent<UIDocument>().rootVisualElement;

        _lobbyBackImg = root_Background.Q<VisualElement>("BackgroundImage");
        _crtTab = 4;
        _tabs = new VisualElement[]{
            root_LobbyUI.Q<VisualElement>("Tab0_Shop"),
            root_LobbyUI.Q<VisualElement>("Tab1_Leniz"),
            root_LobbyUI.Q<VisualElement>("Tab2_Item"),
            root_LobbyUI.Q<VisualElement>("Tab3_Lab"),
            root_LobbyUI.Q<VisualElement>("Tab4_Lobby"),
            root_LobbyUI.Q<VisualElement>("Tab5_Gacha"),
            root_LobbyUI.Q<VisualElement>("Tab6_House"),
            root_LobbyUI.Q<VisualElement>("Tab7_Guild"),
            root_LobbyUI.Q<VisualElement>("Tab8_Square"),
        };
        _tabBtns = new Button[]{
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
        foreach (var _tabBtn in _tabBtns)
        {
            _tabBtn.RegisterCallback<ClickEvent>(OnBottomTabBtnClicked);
        }
        SetupYoyo();
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
        print(yoyoElements.Count);
    }

    private void OnBottomTabBtnClicked(ClickEvent evt)
    {
        _newTab = Array.IndexOf(_tabBtns, evt.currentTarget);

        _tabs[_newTab].style.display = DisplayStyle.Flex;
        //if(_subTabs[_newTab] != null) _subTabs[_newTab].style.display = DisplayStyle.Flex;

        var oldTab = _tabs[_crtTab];

        EventCallback<TransitionEndEvent> onTransitionEnd = null;
        onTransitionEnd = (TransitionEndEvent e) =>
        {
            oldTab.style.display = DisplayStyle.None;
            oldTab.UnregisterCallback(onTransitionEnd);
        };

        oldTab.RegisterCallback(onTransitionEnd);

        //if (_subTabs[_crtTab] != null)
        //{
        //    var oldSubTab = _subTabs[_crtTab];

        //    EventCallback<TransitionEndEvent> onTransitionEnd_sub = null;
        //    onTransitionEnd_sub = (TransitionEndEvent e) =>
        //    {
        //        oldSubTab.style.display = DisplayStyle.None;
        //        oldSubTab.UnregisterCallback(onTransitionEnd_sub);
        //    };

        //    oldSubTab.RegisterCallback(onTransitionEnd_sub);
        //}

        for (int i = 0; i < _tabs.Length; i++)
        {
            if (i < _newTab)
            {
                if (!_tabs[i].ClassListContains("tab--left"))
                    _tabs[i].AddToClassList("tab--left");
                if (_tabs[i].ClassListContains("tab--right"))
                    _tabs[i].RemoveFromClassList("tab--right");
                //if (_subTabs[i] != null)
                //{
                //    if (!_subTabs[i].ClassListContains("tab--left"))
                //        _subTabs[i].AddToClassList("tab--left");
                //    if (_subTabs[i].ClassListContains("tab--right"))
                //        _subTabs[i].RemoveFromClassList("tab--right");
                //}
            }
            else if (i == _newTab)
            {
                if (_tabs[i].ClassListContains("tab--left"))
                    _tabs[i].RemoveFromClassList("tab--left");
                if (_tabs[i].ClassListContains("tab--right"))
                    _tabs[i].RemoveFromClassList("tab--right");
                //if (_subTabs[i] != null)
                //{
                //    if (_subTabs[i].ClassListContains("tab--left"))
                //        _subTabs[i].RemoveFromClassList("tab--left");
                //    if (_subTabs[i].ClassListContains("tab--right"))
                //        _subTabs[i].RemoveFromClassList("tab--right");
                //}
            }
            else
            {
                if (_tabs[i].ClassListContains("tab--left"))
                    _tabs[i].RemoveFromClassList("tab--left");
                if (!_tabs[i].ClassListContains("tab--right"))
                    _tabs[i].AddToClassList("tab--right");
                //if (_subTabs[i] != null)
                //{
                //    if (_subTabs[i].ClassListContains("tab--left"))
                //        _subTabs[i].RemoveFromClassList("tab--left");
                //    if (!_subTabs[i].ClassListContains("tab--right"))
                //        _subTabs[i].AddToClassList("tab--right");
                //}
            }
        }
        _crtTab = _newTab;
        //_lobbyBackImg.style.translate = new Translate(new Length(0f+(_newTab-4f)*2f, LengthUnit.Percent), 0f);
    }
}
