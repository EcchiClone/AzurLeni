using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    // °¡Ã­ ¹öÆ°
    private VisualElement _characterImage;
    // °¡Ã­ ¹öÆ°
    private Button _btnGatcha;
    private Button _toLobby;
    private Button _toCharacterLeft;
    private Button _toCharacterRight;
    private Button _toCharacterCenter;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _characterImage = root.Q<VisualElement>("Character");
        _btnGatcha = root.Q<Button>("Button_GatchaTab");
        _toLobby = root.Q<Button>("Button_ToLobby");
        _toCharacterLeft = root.Q<Button>("Button_Left");
        _toCharacterCenter = root.Q<Button>("Button_Center");
        _toCharacterRight = root.Q<Button>("Button_Right");

        _btnGatcha.RegisterCallback<ClickEvent>(OnGatchaTabBtnClicked);
        _toLobby.RegisterCallback<ClickEvent>(OnLobbyBtnClicked);
        _toCharacterLeft.RegisterCallback<ClickEvent>(CharacterLeft);
        _toCharacterCenter.RegisterCallback<ClickEvent>(CharacterCenter);
        _toCharacterRight.RegisterCallback<ClickEvent>(CharacterRight);
    }

    private void OnGatchaTabBtnClicked(ClickEvent evt)
    {
        _characterImage.style.display = DisplayStyle.None;
    }
    private void OnLobbyBtnClicked(ClickEvent evt)
    {
        _characterImage.style.display = DisplayStyle.Flex;
    }
    private void CharacterLeft(ClickEvent evt)
    {
        _characterImage.RemoveFromClassList("character--right");
        _characterImage.AddToClassList("character--left");
    }
    private void CharacterCenter(ClickEvent evt)
    {
        _characterImage.RemoveFromClassList("character--left");
        _characterImage.RemoveFromClassList("character--right");
    }
    private void CharacterRight(ClickEvent evt)
    {
        _characterImage.RemoveFromClassList("character--left");
        _characterImage.AddToClassList("character--right");
    }
}
