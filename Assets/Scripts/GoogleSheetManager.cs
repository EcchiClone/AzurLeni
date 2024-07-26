using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[System.Serializable]
public class GoogleData
{
    public string order, result, msg, value;
}
public class UserStats
{
    public string uid, name, have_char, comment;
    public int level, exp;
}
public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbzmo8fkRtX1qF9WTxnI2lxjshyRnkU4O87IaCit2vz9aCxoPX2eX-JImUzBq3r_M3J2/exec";
    public GoogleData GD;
    public UserStats US;
    public TMP_InputField IdInput, PassInput, ValueInput;
    public TMP_Text UserStats;
    string id, pass;
    private string uid;

    //private IEnumerator Start()
    //{
    //    UnityWebRequest www = UnityWebRequest.Get(URL);
    //    yield return www.SendWebRequest();

    //    string data = www.downloadHandler.text; //
    //    print(data);

    //    WWWForm form = new WWWForm();
    //    form.AddField("key", "키");
    //    form.AddField("value", "값");

    //    UnityWebRequest www2 = UnityWebRequest.Post(URL, form);
    //    yield return www2.SendWebRequest();

    //    string data2 = www2.downloadHandler.text; //
    //    print(data2);
    //}

    public void Register()
    {
        if (!SetIdPass())
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
    public void Login()
    {
        if (!SetIdPass())
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
    public void GetUserStats()
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
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
            else Response(www.downloadHandler.text);
        }
    }
    IEnumerator Get(string query)
    {
        string fullUrl = $"{URL}?{query}";
        Debug.Log(fullUrl);
        using (UnityWebRequest www = UnityWebRequest.Get(fullUrl))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
            else Response(www.downloadHandler.text);
        }
    }
    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;
        Debug.Log(json);
        GD = JsonUtility.FromJson<GoogleData>(json);
        if(GD.result == "ERROR")
        {
            print("!ERROR! : " + GD.msg);
            return;
        }
        print($"{GD.order} : {GD.msg}");
        if (GD.order == "login") LoginTasks();
        if (GD.order == "userStats") VisualizeUserStats();
    }
    void LoginTasks()
    {
        uid = GD.value.ToString();
    }
    void VisualizeUserStats()
    {

        US = JsonUtility.FromJson<UserStats>(GD.value);
        UserStats.text =
            $"UID   | {US.uid}\n" +
            $"Name  | {US.name}\n" +
            $"Level | {US.level}\n" +
            $"EXP   | {US.exp}\n" +
            $"HAVE  | {US.have_char}\n" +
            $"Comment   | {US.comment}";
    }
    bool SetIdPass()
    {
        id = IdInput.text.Trim();
        pass = PassInput.text.Trim();
        if (id == "" || pass == "") return false;
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
