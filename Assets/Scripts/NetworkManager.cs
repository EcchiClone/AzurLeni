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

    public event Action OnLoginCompleted;

    private void Awake()
    {
        if(instance == null) instance = this;
        else { Destroy(this); return; }
        DontDestroyOnLoad(this.gameObject);
    }

    const string SERVER_URL = "https://script.google.com/macros/s/AKfycbzmo8fkRtX1qF9WTxnI2lxjshyRnkU4O87IaCit2vz9aCxoPX2eX-JImUzBq3r_M3J2/exec";

    public void Register(SendForm _sendForm)
    {

        if (!IsSetIdPass(_sendForm.id, _sendForm.password))
        {
            print("아이디 또는 비밀번호가 비어있음");
            return;
        }

        WWWForm form = new WWWForm();

        form.AddField("order", _sendForm.order);
        form.AddField("id", _sendForm.id);
        form.AddField("pass", _sendForm.password);

        StartCoroutine(Post(form));
    }
    public void Login(SendForm _postFrom)
    {
        if (!IsSetIdPass(_postFrom.id, _postFrom.password))
        {
            print("아이디 또는 비밀번호가 비어있음");
            return;
        }

        WWWForm form = new WWWForm();

        form.AddField("order", "login");
        form.AddField("id", _postFrom.id);
        form.AddField("pass", _postFrom.password);

        StartCoroutine(Post(form));
    }
    public void RequestUserData() // Query 형식의 Get 양식
    {
        string[] _queryElements = new string[]
        {
            $"uid={DataManager.CurrentUserData.uid}",
            $"order=userData",
        };
        StartCoroutine(Get(CombineQueries(_queryElements)));
    }

    public string CombineQueries(string[] _queryElements)
    {
        StringBuilder _out = new StringBuilder();
        for (int i = 0; i < _queryElements.Length; i++)
        {
            _out.Append(_queryElements[i]);
            if (i < _queryElements.Length - 1)
            {
                _out.Append("&");
            }
        }
        return _out.ToString();
    }
    IEnumerator Post(WWWForm _form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(SERVER_URL, _form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
            else Response(www.downloadHandler.text);
        }
    }
    IEnumerator Get(string _queryString)
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

        if(responseData.result == "ERROR") { print("!ERROR! : " + responseData.msg); return; }

        switch (responseData.order)
        {
            case "login":       LoginTasks(responseData); break;
            case "userData":    SetCurrentUserData(responseData); break;
        }
    }
    void LoginTasks(ResponseData _responseData)
    {
        DataManager.LoadUserID(_responseData.value.ToString());
        RequestUserData();
    }
    void SetCurrentUserData(ResponseData _responseData)
    {
        DataManager.LoadUserData(_responseData.value);
        OnLoginCompleted?.Invoke();
    }
    bool IsSetIdPass(string _id, string _password)
    {
        if (_id.Trim() == "" || _password.Trim() == "") return false;
        else return true;
    }
    private void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("uid", DataManager.CurrentUserData.uid);
        form.AddField("order", "logout");
        StartCoroutine(Post(form));
    }
}
