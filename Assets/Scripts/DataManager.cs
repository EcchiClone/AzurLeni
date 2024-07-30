using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData // 유저당 한 개의 프로필
{
    public string uid, name, haveChar, comment;
    public int level, exp;
}
public class DataManager : MonoBehaviour
{
    // 싱글톤
    public static DataManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static UserData CurrentUserData { get; private set; }

    public static void LoadUserData(string json)
    {
        CurrentUserData = JsonUtility.FromJson<UserData>(json);
    }
}
