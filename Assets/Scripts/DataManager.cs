using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public static UserState CurrentUserState = UserState.LoggedOut;
    public static UserData CurrentUserData { get; private set; } = new UserData();

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this); return; }
        DontDestroyOnLoad(this.gameObject);
    }

    public static void LoadUserID(string _uid)
    {
        CurrentUserData.uid = _uid;
    }
    public static void LoadUserData(string _json)
    {
        CurrentUserData = JsonUtility.FromJson<UserData>(_json);
    }
    public static void ClearUserData()
    {
        CurrentUserData = new UserData();
    }
}
