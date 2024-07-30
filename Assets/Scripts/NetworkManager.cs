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
    const string SERVER_URL = "https://script.google.com/macros/s/AKfycbzmo8fkRtX1qF9WTxnI2lxjshyRnkU4O87IaCit2vz9aCxoPX2eX-JImUzBq3r_M3J2/exec";

    public TMP_InputField Input_ID, Input_Password, ValueInput;
    public TMP_Text PlayerDataText;
    string id, pass;
    private string uid;

    public void Register(SendForm _postFrom)
    {

        if (!SetIdPass(_postFrom.id, _postFrom.password))
        {
            print("아이디 또는 비밀번호가 비어있음");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }
    public void Login(SendForm _postFrom)
    {
        if (!SetIdPass(_postFrom.id, _postFrom.password))
        {
            print("아이디 또는 비밀번호가 비어있음");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }
    public void GetUserStats() // Query 형식의 Get 양식
    {
        string[] _qs = new string[]
        {
            $"uid={uid}",
            $"order=userStats",
        };
        StartCoroutine(Get(CombineQueries(_qs)));
    }

    public string CombineQueries(string[] _qs)
    {
        StringBuilder _out = new StringBuilder();
        for (int i = 0; i < _qs.Length; i++)
        {
            _out.Append(_qs[i]);
            if (i < _qs.Length - 1)
            {
                _out.Append("&");
            }
        }
        return _out.ToString();
    }
    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(SERVER_URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
            else Response(www.downloadHandler.text);
        }
    }
    IEnumerator Get(string query)
    {
        string URL_WITH_QUERY = $"{SERVER_URL}?{query}";
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
        Debug.Log(_json);
        ResponseData responseData = JsonUtility.FromJson<ResponseData>(_json);
        if(responseData.result == "ERROR")
        {
            print("!ERROR! : " + responseData.msg);
            return;
        }
        print($"{responseData.order} : {responseData.msg}");
        if (responseData.order == "login") LoginTasks(responseData);
        if (responseData.order == "userStats") VisualizeUserStats(responseData);

    }
    void LoginTasks(ResponseData _responseData)
    {
        uid = _responseData.value.ToString();
    }
    void VisualizeUserStats(ResponseData _responseData)
    {
        DataManager.LoadUserData(_responseData.value);
        PlayerDataText.text =
            $"UID   | {DataManager.CurrentUserData.uid}\n" +
            $"Name  | {DataManager.CurrentUserData.name}\n" +
            $"Level | {DataManager.CurrentUserData.level}\n" +
            $"EXP   | {DataManager.CurrentUserData.exp}\n" +
            $"HaveCharacter  | {DataManager.CurrentUserData.haveChar}\n" +
            $"Comment   | {DataManager.CurrentUserData.comment}";
    }
    bool SetIdPass(string _id, string _password)
    {
        if (_id.Trim() == "" || _password.Trim() == "") return false;
        else return true;
    }
    private void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("uid", uid);
        form.AddField("order", "logout");
        StartCoroutine(Post(form));
    }

    public void SetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("uid", uid);
        form.AddField("order", "setValue");
        form.AddField("value", ValueInput.text.Trim());
        StartCoroutine(Post(form));
    }
    public void GetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("uid", uid);
        form.AddField("order", "getValue");
        StartCoroutine(Post(form));
    }
}
