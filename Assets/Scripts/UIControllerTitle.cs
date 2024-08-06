using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIControllerTitle : MonoBehaviour
{
    // 컴포넌트 클래스, Root VisualElement
    public UIDocument UI_Title;
    private VisualElement root_title;

    // TextField, Buttons
    private TextField IDTextField;
    private TextField PasswordTextField;
    private Button RegisterButton;
    private Button LoginButton;
    private Button LobbyButton;
    private Button LogoutButton;

    private Label UserDataLabel;

    private void Start()
    {
        root_title = UI_Title.GetComponent<UIDocument>().rootVisualElement;

        IDTextField = root_title.Q<TextField>("IDTextField");
        PasswordTextField = root_title.Q<TextField>("PasswordTextField");

        RegisterButton = root_title.Q<Button>("RegisterButton");
        LoginButton = root_title.Q<Button>("LoginButton");
        LobbyButton = root_title.Q<Button>("LobbyButton");
        LogoutButton = root_title.Q<Button>("LogoutButton");

        UserDataLabel = root_title.Q<Label>("UserData");

        RegisterButton.RegisterCallback<ClickEvent>(OnRegisterButtonClicked);
        LoginButton.RegisterCallback<ClickEvent>(OnLoginButtonClicked);
        LobbyButton.RegisterCallback<ClickEvent>(OnLobbyButtonClicked);
        LogoutButton.RegisterCallback<ClickEvent>(OnLogoutButtonClicked);

        NetworkManager.instance.OnLoginCompleted += FlexLoginSuccessElements;
        NetworkManager.instance.OnLogoutCompleted += FlexForLoginElements;

        NetworkManager.instance.OnGetUserDataCompleted += UserDataLabelUpdate;
    }

    public void OnRegisterButtonClicked(ClickEvent evt)
    {
        if (IDTextField.text.Trim() == "" || PasswordTextField.text.Trim() == "")
        {
            print("아이디 또는 비밀번호가 비어있음");
            LogText.AddLog("아이디 또는 비밀번호가 비어있음", LogSign.Warning);
            return;
        }
        SendForm sendForm = new SendForm()
        {
            order = "register",
            id = IDTextField.text,
            password = PasswordTextField.text,
        };
        NetworkManager.instance.Post(sendForm);
    }

    public void OnLoginButtonClicked(ClickEvent evt)
    {
        if (IDTextField.text.Trim() == "" || PasswordTextField.text.Trim() == "")
        {
            print("아이디 또는 비밀번호가 비어있음");
            LogText.AddLog("아이디 또는 비밀번호가 비어있음", LogSign.Warning);
            return;
        }
        SendForm sendForm = new SendForm()
        {
            order = "login",
            id = IDTextField.text,
            password = PasswordTextField.text,
        };
        NetworkManager.instance.Post(sendForm);
    }
    public void OnLobbyButtonClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("Lobby");
    }
    public void OnLogoutButtonClicked(ClickEvent evt)
    {
        SendForm sendForm = new SendForm()
        {
            uid = DataManager.CurrentUserData.uid,
            order = "logout",
        };
        NetworkManager.instance.Post(sendForm);
    }
    private void UserDataLabelUpdate()
    {
        UserDataLabel.text =
            $"{DataManager.CurrentUserData.uid}" + "\n" +
            $"{DataManager.CurrentUserData.name}" + "\n" +
            $"{DataManager.CurrentUserData.level}";
    }

    private void FlexLoginSuccessElements()
    {
        root_title.Q<VisualElement>("ForLoginElements").style.display = DisplayStyle.None;
        root_title.Q<VisualElement>("LoginSuccessElements").style.display = DisplayStyle.Flex;
    }

    private void FlexForLoginElements()
    {
        UserDataLabel.text = "";
        root_title.Q<VisualElement>("LoginSuccessElements").style.display = DisplayStyle.None;
        root_title.Q<VisualElement>("ForLoginElements").style.display = DisplayStyle.Flex;
    }

}
