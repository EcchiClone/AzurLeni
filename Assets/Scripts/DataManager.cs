using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public static UserState _UserState = UserState.LoggedOut;
    public static UserData _UserData { get; private set; } = new UserData();
    public static GameData _GameData { get; private set; } = new GameData();


    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadUserID(string _uid)
    {
        _UserData.uid = _uid;
    }
    public static void LoadUserData(string _json)
    {
        //_UserData = JsonUtility.FromJson<UserData>(_json);
        print(_json);
        _UserData = JsonConvert.DeserializeObject<UserData>(_json);
        print(_UserData);
        print(_UserData.inventory);
        print(_UserData.inventory.item_general);
        print(_UserData.character);
        print(_UserData.character[0]);
        DataManager.instance.UpdateUserData();
    }
    public static void LoadGameData(string _json)
    {
        print(_json);
        _GameData = JsonConvert.DeserializeObject<GameData>(_json);
        print(_GameData);
        print(_GameData.version);
        print(_GameData.totalCharacterCount);
    }
    public static void ClearUserData()
    {
        _UserData = new UserData();
    }
    private void UpdateUserData()
    {
        for (int i = 0; i < DataManager._GameData.totalCharacterCount; i++)
        {
            // id가 i와 일치하는 캐릭터가 존재하는지 확인
            bool characterExists = DataManager._UserData.character.Exists(character => character.id == i);

            if (!characterExists)
            {
                // id가 i인 캐릭터가 없다면 새로운 UserCharacter를 생성하여 추가
                UserCharacter newCharacter = new UserCharacter
                {
                    id = i,
                };

                DataManager._UserData.character.Add(newCharacter);
            }
        }

        DataManager._UserData.character.Sort((a,b) => a.id.CompareTo(b.id));
        foreach(var character in DataManager._UserData.character)
        {
            print(character.id);
        }

    }

}
