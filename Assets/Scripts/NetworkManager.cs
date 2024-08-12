using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendForm
{
    public string uid;
    public string order;
    public string value;

    public string id;
    public string password;
}
public class ResponseData
{
    public string order;
    public string result;
    public string value;
    public string msg;
}

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public event Action OnRegisterCompleted;
    public event Action OnLoginCompleted;
    public event Action OnLogoutCompleted;
    public event Action OnGetUserDataCompleted;

    private void Awake()
    {
        if(instance == null) instance = this;
        else { Destroy(this); return; }
        DontDestroyOnLoad(this.gameObject);
    }

    const string SERVER_URL = "https://script.google.com/macros/s/AKfycbzmo8fkRtX1qF9WTxnI2lxjshyRnkU4O87IaCit2vz9aCxoPX2eX-JImUzBq3r_M3J2/exec";

    public void Post(SendForm _form)
    {
        WWWForm form = new WWWForm();

        AddFieldIfNotNull(form, "uid", _form.uid);
        AddFieldIfNotNull(form, "order", _form.order);
        AddFieldIfNotNull(form, "value", _form.value);
        AddFieldIfNotNull(form, "id", _form.id);
        AddFieldIfNotNull(form, "pass", _form.password);

        StartCoroutine(ExecutePost(form));
    }

    private void AddFieldIfNotNull(WWWForm form, string fieldName, string fieldValue)
    {
        if (fieldValue != null)
        {
            form.AddField(fieldName, fieldValue);
        }
    }

    public void Get(SendForm _form)
    {
        string query = BuildQuery(_form);
        StartCoroutine(ExecuteGet(query));
    }

    private string BuildQuery(SendForm _form)
    {
        StringBuilder query = new StringBuilder();
        bool first = true;

        void AddQueryParam(string name, string value)
        {
            if (value != null)
            {
                if (!first)
                {
                    query.Append("&");
                }
                query.Append($"{name}={UnityWebRequest.EscapeURL(value)}");
                first = false;
            }
        }

        AddQueryParam("uid", _form.uid);
        AddQueryParam("order", _form.order);
        AddQueryParam("value", _form.value);
        AddQueryParam("id", _form.id);
        AddQueryParam("pass", _form.password);

        return query.ToString();
    }

    IEnumerator ExecutePost(WWWForm _form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(SERVER_URL, _form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
            else Response(www.downloadHandler.text);
        }
    }
    IEnumerator ExecuteGet(string _queryString)
    {
        string URL_WITH_QUERY = $"{SERVER_URL}?{_queryString}";
        using (UnityWebRequest www = UnityWebRequest.Get(URL_WITH_QUERY))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
            else Response(www.downloadHandler.text);
        }
    }
    void Response(string _json)
    {
        if (string.IsNullOrEmpty(_json)) return;
        print(_json); // 받아온 내용 Debug

        ResponseData responseData = JsonUtility.FromJson<ResponseData>(_json);

        if (responseData.result == "ERROR")
        {
            print("!ERROR! : " + responseData.msg);
            LogText.AddLog(responseData.msg, LogSign.Error);
            return;
        }

        switch (responseData.order)
        {
            case "register":    RegisterTasks(responseData); break;
            case "login":       LoginTasks(responseData); break;
            case "logout":      LogoutTasks(responseData); break;

            case "userData":    SetCurrentUserData(responseData); break;
        }
    }
    void RegisterTasks(ResponseData _responseData)
    {
        LogText.AddLog(_responseData.msg);
        OnRegisterCompleted?.Invoke();
    }
    void LoginTasks(ResponseData _responseData)
    {
        DataManager.LoadUserID(_responseData.value.ToString());
        LogText.AddLog(_responseData.msg);
        OnLoginCompleted?.Invoke();

        LogText.AddLog("유저의 게임 데이터 요청 시도");
        Get(new SendForm() { order = "userData", uid = DataManager.CurrentUserData.uid });
    }
    void LogoutTasks(ResponseData _responseData)
    {
        DataManager.ClearUserData();
        LogText.AddLog(_responseData.msg);
        OnLogoutCompleted?.Invoke();
    }
    void SetCurrentUserData(ResponseData _responseData)
    {
        DataManager.LoadUserData(_responseData.value);
        LogText.AddLog(_responseData.msg);
        OnGetUserDataCompleted?.Invoke();
    }

    private void OnApplicationQuit()
    {
        SendForm sendForm = new SendForm()
        {
            uid = DataManager.CurrentUserData.uid,
            order = "logout",
        };
        Post(sendForm);
    }
}
