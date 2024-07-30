using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField idInput, passwordInput;
    [SerializeField] private TMP_Text UserDataText;
    [SerializeField] private GameObject[] DisableOnLogin;
    [SerializeField] private GameObject[] EnableOnLogin;

    private void Start()
    {
        NetworkManager.instance.OnLoginCompleted += SuccessLoginOnTitle;
    }

    public void LoginBtnClicked()
    {
        SendForm sendForm = new SendForm()
        {
            uid = "",
            order = "login",
            value = "",
            id = idInput.text,
            password = passwordInput.text,
        };
        NetworkManager.instance.Login(sendForm);

    }
    public void SuccessLoginOnTitle()
    {
        UserDataText.text =
            $"UID   | {DataManager.CurrentUserData.uid}\n" +
            $"Name  | {DataManager.CurrentUserData.name}\n" +
            $"Level | {DataManager.CurrentUserData.level}\n" +
            $"EXP   | {DataManager.CurrentUserData.exp}\n" +
            $"HaveCharacter  | {DataManager.CurrentUserData.haveChar}\n" +
            $"Comment   | {DataManager.CurrentUserData.comment}";

        foreach (var item in DisableOnLogin)
        {
            item.SetActive(false);
        }
        foreach (var item in EnableOnLogin)
        {
            item.SetActive(true);
        }
    }

}
