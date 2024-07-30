using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnRegister : MonoBehaviour
{
    [SerializeField] private TMP_InputField idInput, passwordInput;
    
    public void RegisterBtnClicked()
    {
        SendForm sendForm = new SendForm()
        {
            uid = "",
            order = "register",
            value = "",
            id = idInput.text,
            password = passwordInput.text,
        };

        NetworkManager.instance.Register(sendForm);
    }
}
