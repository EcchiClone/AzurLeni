using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    //public UserState _UserState = UserState.LoggedOut;
    public UserData UserData { get; private set; } = new UserData();
    public GameData GameData { get; private set; } = new GameData();

    public void LoadUserID(string _uid)
    {
        UserData.uid = _uid;
    }
    public void LoadUserData(string _json)
    {
        print($"Load User Data: {_json}");
        UserData = JsonConvert.DeserializeObject<UserData>(_json);

        // 데이터 누락 채우기 등
        UpdateUserData();
    }
    public void LoadGameData(string _json)
    {
        print($"Load Game Data: {_json}");
        GameData = JsonConvert.DeserializeObject<GameData>(_json);
        print($"AzurLeni ver: {GameData.version}");
        LogText.AddLog($"AzurLeni ver: {GameData.version}");
        print($"Total Character Count: {GameData.totalCharacterCount}");
    }
    public void ClearUserData()
    {
        UserData = new UserData();
    }
    private void UpdateUserData()
    {
        for (int i = 0; i < Game.GameData.totalCharacterCount; i++)
        {
            // id가 i와 일치하는 캐릭터가 존재하는지 확인
            bool characterExists = Game.UserData.character.Exists(character => character.id == i);

            if (!characterExists)
            {
                // id가 i인 캐릭터가 없다면 새로운 UserCharacter를 생성하여 추가
                UserCharacter newCharacter = new UserCharacter
                {
                    id = i,
                };

                Game.UserData.character.Add(newCharacter);
            }
        }

        Game.UserData.character.Sort((a,b) => a.id.CompareTo(b.id)); // id 순 정렬
    }

}
